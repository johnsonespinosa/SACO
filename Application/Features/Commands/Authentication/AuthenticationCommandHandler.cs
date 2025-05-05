using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using Application.Abstractions.Interfaces.Services;
using Domain.Authentication;
using Domain.Errors;
using Domain.Models;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.Authentication;

internal sealed class AuthenticationCommandHandler(
    UserManager<User> userManager,
    IRepositoryAsync<RefreshToken> repository,
    ITokenService tokenService,
    ILogger<AuthenticationCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AuthenticationCommand, AuthResponse>
{
    public async Task<Result<AuthResponse>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        // Validación de IP
        if (!IPAddress.TryParse(request.IpAddress, out _))
            return Result.Failure<AuthResponse>(DomainErrors.Auth.InvalidIpFormat);

        var user = await userManager.FindByNameAsync(request.UserName);
        var isValidUser = user != null && await userManager.CheckPasswordAsync(user, request.Password);
        
        if (!isValidUser)
        {
            if (user != null) await userManager.AccessFailedAsync(user);
            logger.LogWarning("Intento fallido: {UserName}", request.UserName);
            return Result.Failure<AuthResponse>(DomainErrors.Auth.InvalidCredentials);
        }

        if (!user!.EmailConfirmed)
            return Result.Failure<AuthResponse>(DomainErrors.Auth.EmailNotConfirmed);
        
        if (user.LockoutEnd > DateTimeOffset.UtcNow)
            return Result.Failure<AuthResponse>(DomainErrors.Auth.AccountLocked);

        try
        {
            return await unitOfWork.ExecuteTransactionalAsync(async (ct) =>
            {
                var token = await tokenService.GenerateAccessToken(user, request.IpAddress);
                var refreshToken = tokenService.GenerateRefreshToken(user.Id, request.IpAddress);

                // Revocar tokens existentes de manera eficiente
                var existingTokens = await repository.ListAsync(
                    new ExistingTokenSpecification(user.Id), 
                    cancellationToken);

                foreach (var existingToken in existingTokens)
                {
                    existingToken.Revoked = DateTime.UtcNow;
                    existingToken.ReplacedByToken = refreshToken.Token;
                    await repository.UpdateAsync(existingToken, ct);
                }

                await repository.AddAsync(refreshToken, ct);
                
                logger.LogInformation("Sesión iniciada: {UserId}", user.Id);
                
                return new AuthResponse(
                    new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken.Token,
                    (int)tokenService.GetTokenDuration().TotalSeconds);
            }, IsolationLevel.ReadCommitted, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error autenticando {UserName}", request.UserName);
            return Result.Failure<AuthResponse>(DomainErrors.Auth.ServerError);
        }
    }
}

