using Domain.Errors;
using FluentValidation;

namespace Application.Features.Commands.Users.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.IdRequired.Code);
    }
}