using Domain.Errors;
using FluentValidation;

namespace Application.Features.Commands.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(expression: command => command.UserName)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.UsernameRequired.Code)
            .WithMessage(DomainErrors.User.UsernameRequired.Detail)
            .MinimumLength(3)
            .WithErrorCode(DomainErrors.User.UsernameTooShort.Code)
            .WithMessage(DomainErrors.User.UsernameTooShort.Detail)
            .MaximumLength(50)
            .WithErrorCode(DomainErrors.User.UsernameTooLong.Code)
            .WithMessage(DomainErrors.User.UsernameTooLong.Detail);

        RuleFor(expression: command => command.Email)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.EmailRequired.Code)
            .WithMessage(DomainErrors.User.EmailRequired.Detail)
            .EmailAddress()
            .WithErrorCode(DomainErrors.User.InvalidEmailFormat.Code)
            .WithMessage(DomainErrors.User.InvalidEmailFormat.Detail);

        RuleFor(expression: command => command.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.PhoneRequired.Code)
            .WithMessage(DomainErrors.User.PhoneRequired.Detail)
            .Matches(expression: @"^\+\d{1,15}$")
            .WithErrorCode(DomainErrors.User.InvalidPhoneFormat.Code)
            .WithMessage(DomainErrors.User.InvalidPhoneFormat.Detail);

        RuleFor(expression: command => command.Password)
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

        RuleFor(expression: command => command.PasswordConfirm)
            .Equal(expression: command => command.Password)
            .WithErrorCode(DomainErrors.User.PasswordsDoNotMatch.Code)
            .WithMessage(DomainErrors.User.PasswordsDoNotMatch.Detail);
    }
}