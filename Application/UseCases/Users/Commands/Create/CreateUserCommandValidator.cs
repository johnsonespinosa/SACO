using Shared.DTOs.Users;

namespace Application.UseCases.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<UserRequest>
{
    public CreateUserCommandValidator()
    {
        RuleFor(expression: request => request.Email)
            .NotEmpty().WithMessage("Se requiere correo electrónico.")
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.");

        RuleFor(expression: request => request.UserName)
            .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
            .Length(3, 50).WithMessage("El nombre de usuario debe tener entre 3 y 50 caracteres.");

        RuleFor(expression: request => request.PhoneNumber)
            .NotEmpty().WithMessage("Se requiere el número de teléfono.")
            .Matches(expression: @"^\+?[1-9]\d{1,14}$").WithMessage("El número de teléfono no es válido.");

        RuleFor(expression: request => request.Password)
            .NotEmpty().WithMessage("Se requiere contraseña.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

        RuleFor(expression: request => request.PasswordConfirm)
            .Equal(expression: request => request.Password).WithMessage("Las contraseñas no coinciden.");
    }
}