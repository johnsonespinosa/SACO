using System.IdentityModel.Tokens.Jwt;
using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs;
using Shared.DTOs.Users;
using Shared.Interfaces;

namespace Identity.Services;

public class UserService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenManager tokenManager,
    IMapper mapper,
    IUserRepository repository)
    : IUserService
{
    public async Task<ServiceResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
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
        return new ServiceResponse<AuthenticationResponse>(Result: authenticationResponse, Succeeded: true,
            Message: ResponseMessages.AuthSuccess);
    }

    public async Task<ServiceResponse<ErrorOr<Unit>>> AddAsync(UserRequest request)
    {
        // Comprobar si el usuario ya existe
        var existingUserByUserName = await userManager.FindByNameAsync(request.UserName);
        if (existingUserByUserName != null)
            Error.Conflict(code: "CreateUser.Conflict", description: ResponseMessages.ResourceExists);

        var existingUserByEmail = await userManager.FindByEmailAsync(request.Email);
        if (existingUserByEmail != null)
            Error.Conflict(code: "CreateUser.Conflict", description: ResponseMessages.ResourceExists);

        var user = mapper.Map<User>(request);
        user.PhoneNumberConfirmed = true;
        user.EmailConfirmed = true;

        // Intenta crear el usuario
        var result = await userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            // Asignar roles al usuario
            await userManager.AddToRoleAsync(user, role: Roles.Operador.ToString());
            return new ServiceResponse<ErrorOr<Unit>>(
                Result: Unit.Value, Succeeded: true, Message: ResponseMessages.CreateSuccess);
        }
        return new ServiceResponse<ErrorOr<Unit>>(
            Result: Error.Failure(code: "CreateUser.Failure", description: ResponseMessages.CreateFailure),
            Succeeded: false,
            Message: ResponseMessages.CreateFailure);
    }

    public async Task<ServiceResponse<ErrorOr<Unit>>> UpdateAsync(UserRequest request)
    {
        var user = await userManager.FindByIdAsync(request.Id);

        mapper.Map(request, user);

        var result = await userManager.UpdateAsync(user!);

        if (!result.Succeeded)
            Error.Failure(code: "UserUpdate.Failure", description: ResponseMessages.UpdateFailure);

        return new ServiceResponse<ErrorOr<Unit>>(
            Result: Unit.Value,
            Succeeded: true, 
            Message: ResponseMessages.UpdateSuccess);
    }

    public async Task<ServiceResponse<ErrorOr<Unit>>> DeleteAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                Error.Failure(code: "UserDelete.Failure", description: ResponseMessages.DeleteFailure);
        }

        return new ServiceResponse<ErrorOr<Unit>>(
            Result: Unit.Value,
            Succeeded: true,
            Message: ResponseMessages.DeleteSuccess);
    }

    public async Task<ServiceResponse<IReadOnlyCollection<UserResponse>>> GetAllAsync(string filterRequest)
    {
        var specification = new GetAllUserSpecification(filterRequest);
        var users = await repository.ListAsync(specification);
        if (!users.Any())
            Error.NotFound(code: "GetUsers.NotFound", description: ResponseMessages.ReadNotFound);
        
        var mapped = mapper.Map<List<UserResponse>>(users);
        return new ServiceResponse<IReadOnlyCollection<UserResponse>>(
            Result: mapped,
            Succeeded: true,
            Message: ResponseMessages.ReadSuccess);
    }

    public async Task<ServiceResponse<UserResponse>> GetById(string id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user != null)
        {
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                Error.Failure(code: "UserDelete.Failure", description: ResponseMessages.DeleteFailure);
        }
        var mapped = mapper.Map<UserResponse>(user);
        return new ServiceResponse<UserResponse>(
            Result: mapped,
            Succeeded: true,
            Message: ResponseMessages.ReadSuccess);
    }
}