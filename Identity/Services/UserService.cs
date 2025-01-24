using System.IdentityModel.Tokens.Jwt;
using Application.DTOs.Users;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services;

public class UserService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenManager tokenManager,
    IMapper mapper,
    IUserRepository repository)
    : IUserService
{
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);
        if (user is null)
            throw new Exception(message: string.Format(ResponseMessages.UserNotFoundByEmail, request.Email!));

        var signInResult = await signInManager.PasswordSignInAsync(user.UserName!, request.Password!, isPersistent: false, lockoutOnFailure: false);
        if (!signInResult.Succeeded)
            throw new Exception(message: string.Format(ResponseMessages.InvalidCredentials, user.Email));

        var securityToken = await tokenManager.GenerateJwtSecurityToken(user);
        var roles = await userManager.GetRolesAsync(user);
        var refreshSecurityToken = tokenManager.GenerateRefreshJwtSecurityToken(ipAddress);

        var authenticationResponse = new AuthenticationResponse()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            JwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Roles = roles.ToList(),
            IsVerified = user.EmailConfirmed,
            RefreshJwtSecurityToken = refreshSecurityToken.JwtSecurityToken
        };
        return authenticationResponse;
    }

    public async Task<ErrorOr<Unit>> AddAsync(CreateUserRequest request)
    {
        // Comprobar si el usuario ya existe
        var existingUserByUserName = await userManager.FindByNameAsync(request.UserName);
        if (existingUserByUserName != null)
            return Error.Conflict(code: "CreateUser.Conflict", description: ResponseMessages.ResourceExists);

        var existingUserByEmail = await userManager.FindByEmailAsync(request.Email);
        if (existingUserByEmail != null)
            return Error.Conflict(code: "CreateUser.Conflict", description: ResponseMessages.ResourceExists);

        var user = mapper.Map<User>(request);
        user.PhoneNumberConfirmed = true;
        user.EmailConfirmed = true;

        // Intenta crear el usuario
        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            // Asignar roles al usuario
            await userManager.AddToRoleAsync(user, role: Roles.Operador.ToString());
            return Unit.Value;
        }
        return Error.Failure(code: "CreateUser.Failure", description: ResponseMessages.CreateFailure);
    }

    public async Task<ErrorOr<Unit>> UpdateAsync(UpdateUserRequest request)
    {
        var user = await userManager.FindByIdAsync(request.Id!);

        mapper.Map(request, user);

        var result = await userManager.UpdateAsync(user!);

        if (!result.Succeeded)
            return Error.Failure(code: "UserUpdate.Failure", description: ResponseMessages.UpdateFailure);

        return Unit.Value;
    }

    public async Task<ErrorOr<Unit>> DeleteAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        var result = await userManager.DeleteAsync(user!);
        if (!result.Succeeded)
            Error.Failure(code: "UserDelete.Failure", description: ResponseMessages.DeleteFailure);
        return Unit.Value;
    }

    public async Task<IReadOnlyCollection<UserResponse>> GetAllAsync()
    {
        var users = await repository.ListAsync();
        var mapped = mapper.Map<List<UserResponse>>(users);
        return mapped;
    }
}