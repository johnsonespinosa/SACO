using FluentValidation;

namespace Application.Features.Commands.Circulations.Delete;

public class DeleteCirculationCommandValidator : AbstractValidator<DeleteCirculationCommand>
{
    public DeleteCirculationCommandValidator()
    {
        RuleFor(expression: command => command.CirculationId).NotEmpty();
    }
}