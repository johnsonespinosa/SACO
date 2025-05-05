using Domain.Errors;
using FluentValidation;

namespace Application.Features.Commands.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(expression: command => command.Id)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.IdRequired.Code);

        RuleFor(expression: command => command.UserName)
            .NotEmpty().WithErrorCode(DomainErrors.User.UsernameRequired.Code)
            .MinimumLength(3).WithErrorCode(DomainErrors.User.UsernameTooShort.Code)
            .MaximumLength(50).WithErrorCode(DomainErrors.User.UsernameTooLong.Code);

        RuleFor(expression: command => command.Email)
            .NotEmpty().WithErrorCode(DomainErrors.User.EmailRequired.Code)
            .EmailAddress().WithErrorCode(DomainErrors.User.InvalidEmailFormat.Code);

        RuleFor(expression: command => command.PhoneNumber)
            .NotEmpty().WithErrorCode(DomainErrors.User.PhoneRequired.Code)
            .Matches(expression: @"^\+\d{1,15}$").WithErrorCode(DomainErrors.User.InvalidPhoneFormat.Code);
        
        RuleFor(expression: command => command.CurrentPassword)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.PasswordRequired.Code)
            .WithMessage(DomainErrors.User.PasswordRequired.Detail)
            .MinimumLength(8)
            .WithErrorCode(DomainErrors.User.PasswordTooShort.Code)
            .WithMessage(DomainErrors.User.PasswordTooShort.Detail)
            .Matches(expression: "[A-Z]").WithErrorCode(DomainErrors.User.PasswordMissingUppercase.Code)
            .WithMessage(DomainErrors.User.PasswordMissingUppercase.Detail)
            .Matches(expression: "[a-z]").WithErrorCode(DomainErrors.User.PasswordMissingLowercase.Code)
            .WithMessage(DomainErrors.User.PasswordMissingLowercase.Detail)
            .Matches(expression: "[0-9]").WithErrorCode(DomainErrors.User.PasswordMissingNumber.Code)
            .WithMessage(DomainErrors.User.PasswordMissingNumber.Detail);
        
        RuleFor(expression: command => command.NewPassword)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.PasswordRequired.Code)
            .WithMessage(DomainErrors.User.PasswordRequired.Detail)
            .MinimumLength(8)
            .WithErrorCode(DomainErrors.User.PasswordTooShort.Code)
            .WithMessage(DomainErrors.User.PasswordTooShort.Detail)
            .Matches(expression: "[A-Z]").WithErrorCode(DomainErrors.User.PasswordMissingUppercase.Code)
            .WithMessage(DomainErrors.User.PasswordMissingUppercase.Detail)
            .Matches(expression: "[a-z]").WithErrorCode(DomainErrors.User.PasswordMissingLowercase.Code)
            .WithMessage(DomainErrors.User.PasswordMissingLowercase.Detail)
            .Matches(expression: "[0-9]").WithErrorCode(DomainErrors.User.PasswordMissingNumber.Code)
            .WithMessage(DomainErrors.User.PasswordMissingNumber.Detail);
    }
}