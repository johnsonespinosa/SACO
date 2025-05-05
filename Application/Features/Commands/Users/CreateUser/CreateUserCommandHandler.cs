using Application.Abstractions.Interfaces.Messaging;
using AutoMapper;
using Domain.Constants;
using Domain.Errors;
using Domain.Models;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Users.CreateUser;

internal sealed class CreateUserCommandHandler(UserManager<User> userManager, IMapper mapper)
    : ICommandHandler<CreateUserCommand, string>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Comprobar si el usuario ya existe
        var existingUserByUserName = await userManager.FindByNameAsync(request.UserName);
        if (existingUserByUserName != null)
            return Result.Failure<string>(DomainErrors.User.UsernameAlreadyExists);

        var existingUserByEmail = await userManager.FindByEmailAsync(request.Email);
        if (existingUserByEmail != null)
            return Result.Failure<string>(DomainErrors.User.EmailAlreadyExists);
        
        if (request.Password != request.PasswordConfirm)
            return Result.Failure<string>(DomainErrors.User.PasswordsDoNotMatch);

        var user = mapper.Map<User>(request);
        user.PhoneNumberConfirmed = true;
        user.EmailConfirmed = true;

        // Intenta crear el usuario
        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
            return Result.Failure<string>(DomainErrors.User.CreationFailed);

        // Asignar roles al usuario
        var roleResult = await userManager.AddToRoleAsync(user, role: RoleNames.Operator);
        if (!roleResult.Succeeded)
            return Result.Failure<string>(DomainErrors.User.RoleAssignmentFailed);
        
        return Result.Success(user.Id);
    }
}