using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using Domain.Circulations;
using Domain.Errors;
using Domain.Models;

namespace Application.Features.Commands.Circulations.Delete;

public record DeleteCirculationCommand(Guid CirculationId) : ICommand;

public class Handler(IRepositoryAsync<Circulation> repository) : ICommandHandler<DeleteCirculationCommand>
{
    public async Task<Result> Handle(DeleteCirculationCommand command, CancellationToken cancellationToken)
    {
        var circulation = await repository.GetByIdAsync(command.CirculationId, cancellationToken);

        if (circulation == null)
            return Result.Failure(DomainErrors.Circulation.NotFound);

        await repository.DeleteAsync(circulation, cancellationToken);
            
        return Result.Success();
    }
}