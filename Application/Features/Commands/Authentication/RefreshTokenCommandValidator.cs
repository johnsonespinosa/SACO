using Domain.Errors;
using FluentValidation;

namespace Application.Features.Commands.Authentication;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty()
            .WithErrorCode(DomainErrors.Auth.InvalidToken.Code)
            .WithMessage(DomainErrors.Auth.InvalidToken.Detail);

        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithErrorCode(DomainErrors.Auth.InvalidRefreshToken.Code)
            .WithMessage(DomainErrors.Auth.InvalidRefreshToken.Detail);

        RuleFor(x => x.IpAddress)
            .NotEmpty()
            .Matches(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$|^([0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$")
            .WithErrorCode(DomainErrors.Auth.InvalidIpFormat.Code)
            .WithMessage(DomainErrors.Auth.InvalidIpFormat.Detail);
    }
}