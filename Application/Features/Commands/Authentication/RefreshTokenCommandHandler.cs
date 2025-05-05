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

internal sealed class RefreshTokenCommandHandler(
    UserManager<User> userManager,
    IRepositoryAsync<RefreshToken> repository,
    ITokenService tokenService,
    ILogger<RefreshTokenCommandHandler> logger,
    IUnitOfWork unitOfWork) : ICommandHandler<RefreshTokenCommand, AuthResponse>
{
    public async Task<Result<AuthResponse>> Handle(
        RefreshTokenCommand request, 
        CancellationToken cancellationToken)
    {
        // Validar IP
        if (!IPAddress.TryParse(request.IpAddress, out _))
            return Result.Failure<AuthResponse>(DomainErrors.Auth.InvalidIpFormat);

        // Obtener claims del token expirado
        var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var userId = principal.FindFirst("uid")?.Value;

        if (string.IsNullOrEmpty(userId))
            return Result.Failure<AuthResponse>(DomainErrors.Auth.InvalidToken);

        // Buscar usuario
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return Result.Failure<AuthResponse>(DomainErrors.Auth.UserNotFound);

        // Validar refresh token
        var refreshToken = await repository.FirstOrDefaultAsync(
            new ActiveRefreshTokenSpecification(request.RefreshToken, userId), 
            cancellationToken);

        if (refreshToken == null || refreshToken.IsExpired || refreshToken.IsRevoked)
            return Result.Failure<AuthResponse>(DomainErrors.Auth.InvalidRefreshToken);

        try
        {
            return await unitOfWork.ExecuteTransactionalAsync(async (ct) =>
            {
                // Generar nuevos tokens
                var newAccessToken = await tokenService.GenerateAccessToken(user, request.IpAddress);
                var newRefreshToken = tokenService.GenerateRefreshToken(user.Id, request.IpAddress);

                // Revocar tokens anteriores
                refreshToken.Revoked = DateTime.UtcNow;
                refreshToken.ReplacedByToken = newRefreshToken.Token;
                await repository.UpdateAsync(refreshToken, ct);
                
                // Agregar nuevo refresh token
                await repository.AddAsync(newRefreshToken, ct);

                logger.LogInformation("Token actualizado: {UserId}", user.Id);

                return new AuthResponse(
                    new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    newRefreshToken.Token,
                    (int)tokenService.GetTokenDuration().TotalSeconds);
            }, IsolationLevel.ReadCommitted, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error actualizando token");
            return Result.Failure<AuthResponse>(DomainErrors.Auth.ServerError);
        }
    }
}