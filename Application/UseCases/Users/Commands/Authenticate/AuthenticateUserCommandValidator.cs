using Application.DTOs.Users;

namespace Application.UseCases.Users.Commands.Authenticate;

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(expression: request => request.Email)
            .NotEmpty().WithMessage("Se requiere correo electrónico.")
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.");

        RuleFor(expression: request => request.Password)
            .NotEmpty().WithMessage("Se requiere contraseña.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
    }
}