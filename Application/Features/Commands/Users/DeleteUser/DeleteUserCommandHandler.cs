using Application.Abstractions.Interfaces.Messaging;
using Domain.Errors;
using Domain.Models;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler(
    UserManager<User> userManager) : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken ct)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null)
            return Result.Failure(DomainErrors.User.NotFound);

        var result = await userManager.DeleteAsync(user);
        return result.Succeeded ? 
            Result.Success() : 
            Result.Failure(DomainErrors.User.DeleteFailed);
    }
}