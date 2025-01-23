using Application.DTOs.OperationalCirculations;

namespace Application.UseCases.OperationalCirculations.Commands.Create;

public class CreateOperationalCirculationCommandValidator : AbstractValidator<CreateOperationalCirculationRequest>
    {
        public CreateOperationalCirculationCommandValidator()
        {
            RuleFor(expression: request => request.FirstName)
                .NotEmpty().WithMessage("El primer nombre es requerido.")
                .Length(1, 50).WithMessage("El primer nombre debe tener entre 1 y 50 caracteres.");

            RuleFor( expression: request => request.LastName1)
                .NotEmpty().WithMessage("El primer apellido es requerido.")
                .Length(1, 50).WithMessage("El primer apellido debe tener entre 1 y 50 caracteres.");

            RuleFor(expression: request => request.LastName2)
                .NotEmpty().WithMessage("El segundo apellido es requerido.")
                .Length(0, 50).WithMessage("El segundo apellido debe tener hasta 50 caracteres.");

            RuleFor(expression: request => request.BirthDate)
                .NotEmpty().WithMessage("La fecha de nacimiento es requerida.");

            RuleFor(expression: request => request.Citizenship) 
                .NotEmpty().WithMessage("La ciudadanía es requerida.")
                .Length(1, 100).WithMessage("La ciudadanía debe tener entre 1 y 100 caracteres.");

            RuleFor(expression: request => request.CirculationType)
                .NotEmpty().WithMessage("La circulación es requerida.")
                .Length(1, 100).WithMessage("La circulación debe tener entre 1 y 100 caracteres.");

            RuleFor(expression: request => request.CirculationDate)
                .NotEmpty().WithMessage("La fecha de circulación es requerida.");

            RuleFor(expression: request => request.Section)
                .NotEmpty().WithMessage("La sección es requerida.")
                .Length(1, 50).WithMessage("La sección debe tener entre 1 y 50 caracteres.");

            RuleFor(expression: request => request.Organ)
                .NotEmpty().WithMessage("El órgano es requerida.")
                .Length(1, 100).WithMessage("El órgano debe tener entre 1 y 100 caracteres.");

            RuleFor(expression: request => request.Official)
                .NotEmpty().WithMessage("El oficial es requerido.")
                .Length(1, 100).WithMessage("El oficial debe tener entre 1 y 100 caracteres.");

            RuleFor(expression: request => request.ExpirationDate)
                .NotEmpty().WithMessage("La fecha de expiración es requerida.");

            RuleFor(expression: request => request.Instruction)
                .NotEmpty().WithMessage("La instrucción es requeridas.")
                .Length(1, 500).WithMessage("La instrucción deben tener entre 1 y 500 caracteres.");

            RuleFor(expression: request => request.Phone1)
                .Matches(expression: @"^\+?[0-9\s-]{7,15}$")
                .WithMessage("El primer número de teléfono no es válido.");

            RuleFor(expression: request => request.Phone2)
                .Matches(expression: @"^\+?[0-9\s-]{7,15}$")
                .WithMessage("El segundo número de teléfono no es válido.");
            
            RuleFor(expression: request => request.ReasonForCirculation)
                .NotEmpty().WithMessage("El motivo de la circulación es requerido.")
                .Length(1, 200).WithMessage("El motivo de la circulación debe tener entre 1 y 200 caracteres.");
        }
    }