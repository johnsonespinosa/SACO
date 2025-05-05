using Domain.Errors;
using FluentValidation;

namespace Application.Features.Commands.Authentication;

public class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
{
    public AuthenticationCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.UsernameRequired.Code)
            .WithMessage(DomainErrors.User.UsernameRequired.Detail)
            .MinimumLength(3)
            .WithErrorCode(DomainErrors.User.UsernameTooShort.Code)
            .WithMessage(DomainErrors.User.UsernameTooShort.Detail)
            .MaximumLength(50)
            .WithErrorCode(DomainErrors.User.UsernameTooLong.Code)
            .WithMessage(DomainErrors.User.UsernameTooLong.Detail);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.PasswordRequired.Code)
            .WithMessage(DomainErrors.User.PasswordRequired.Detail)
            .MinimumLength(8)
            .WithErrorCode(DomainErrors.User.PasswordTooShort.Code)
            .WithMessage(DomainErrors.User.PasswordTooShort.Detail)
            .Matches("[A-Z]").WithErrorCode(DomainErrors.User.PasswordMissingUppercase.Code)
            .WithMessage(DomainErrors.User.PasswordMissingUppercase.Detail)
            .Matches("[a-z]").WithErrorCode(DomainErrors.User.PasswordMissingLowercase.Code)
            .WithMessage(DomainErrors.User.PasswordMissingLowercase.Detail)
            .Matches("[0-9]").WithErrorCode(DomainErrors.User.PasswordMissingNumber.Code)
            .WithMessage(DomainErrors.User.PasswordMissingNumber.Detail);

        RuleFor(x => x.IpAddress)
            .NotEmpty()
            .WithErrorCode(DomainErrors.Auth.IpAddressRequired.Code)
            .WithMessage(DomainErrors.Auth.IpAddressRequired.Detail)
            .Matches(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$|^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$")
            .WithErrorCode(DomainErrors.Auth.InvalidIpFormat.Code)
            .WithMessage(DomainErrors.Auth.InvalidIpFormat.Detail);
    }
}