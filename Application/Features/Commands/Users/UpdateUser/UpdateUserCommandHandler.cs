using Application.Abstractions.Interfaces.Messaging;
using AutoMapper;
using Domain.Errors;
using Domain.Models;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(
    UserManager<User> userManager,
    IMapper mapper) : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken ct)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null)
            return Result.Failure(DomainErrors.User.NotFound);
        
        if (!await userManager.CheckPasswordAsync(user, request.CurrentPassword))
            return Result.Failure(DomainErrors.User.InvalidCurrentPassword);
        
        if (!string.IsNullOrEmpty(request.NewPassword))
        {
            var changePasswordResult = await userManager.ChangePasswordAsync(
                user, 
                request.CurrentPassword, 
                request.NewPassword);
        
            if (!changePasswordResult.Succeeded)
                return Result.Failure(changePasswordResult.Errors.ToApplicationErrors()); // ✅ Conversión
        }

        mapper.Map(request, user);
        
        var updateResult = await userManager.UpdateAsync(user);
        return updateResult.Succeeded ? 
            Result.Success() : 
            Result.Failure(updateResult.Errors.ToApplicationErrors()); // ✅ Conversión
    }
}