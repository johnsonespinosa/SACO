using FluentValidation;

namespace Application.Features.Commands.Circulations.Update;

internal sealed class UpdateCirculationCommandValidator : AbstractValidator<UpdateCirculationCommand>
{
    public UpdateCirculationCommandValidator()
    {
        RuleFor(expression: command => command.CirculationId).NotEmpty();
        
        RuleFor(expression: command => command.FirstName)
                .NotEmpty().WithMessage("El primer nombre es requerido.")
                .Length(1, 50).WithMessage("El primer nombre debe tener entre 1 y 50 caracteres.");
            
            RuleFor(expression: command => command.SecondName)
                .Length(0, 50).WithMessage("El segundo nombre debe tener hasta 50 caracteres.");

            RuleFor( expression: command => command.LastName1)
                .NotEmpty().WithMessage("El primer apellido es requerido.")
                .Length(1, 50).WithMessage("El primer apellido debe tener entre 1 y 50 caracteres.");

            RuleFor(expression: command => command.LastName2)
                .Length(0, 50).WithMessage("El segundo apellido debe tener hasta 50 caracteres.");

            RuleFor(expression: command => command.BirthDate)
                .NotEmpty().WithMessage("La fecha de nacimiento es requerida.");

            RuleFor(expression: command => command.CitizenshipId) 
                .NotEmpty().WithMessage("La ciudadanía es requerida.");

            RuleFor(expression: command => command.CirculationTypeId)
                .NotEmpty().WithMessage("La circulación es requerida.");

            RuleFor(expression: command => command.Section)
                .NotEmpty().WithMessage("La sección es requerida.")
                .Length(1, 50).WithMessage("La sección debe tener entre 1 y 50 caracteres.");

            RuleFor(expression: command => command.OrganId)
                .NotEmpty().WithMessage("El órgano es requerida.");

            RuleFor(expression: command => command.Official)
                .NotEmpty().WithMessage("El oficial es requerido.")
                .Length(1, 100).WithMessage("El oficial debe tener entre 1 y 100 caracteres.");

            RuleFor(expression: command => command.ExpirationId)
                .NotEmpty().WithMessage("La fecha de expiración es requerida.");

            RuleFor(expression: command => command.Instruction)
                .NotEmpty().WithMessage("La instrucción es requeridas.")
                .Length(1, 500).WithMessage("La instrucción deben tener entre 1 y 500 caracteres.");
            
            RuleForEach(expression: command => command.PhoneNumbers)
                .Matches(expression: @"^\+?[1-9]\d{1,14}$")
                .WithMessage("Formato de número inválido. Use formato internacional E.164");
            
            RuleFor(expression: command => command.Observations)
                .MaximumLength(1000).WithMessage("Las observaciones deben tener máximo 1000 caracteres");
            
            RuleFor(expression: command => command.ReasonForCirculation)
                .NotEmpty().WithMessage("El motivo de la circulación es requerido.")
                .Length(1, 200).WithMessage("El motivo de la circulación debe tener entre 1 y 200 caracteres.");
    }
}