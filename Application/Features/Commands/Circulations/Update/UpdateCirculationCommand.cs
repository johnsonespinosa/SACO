using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations;
using Domain.Errors;
using Domain.Models;

namespace Application.Features.Commands.Circulations.Update;

public sealed record UpdateCirculationCommand(
    Guid CirculationId,
    string FirstName,
    string LastName1,
    string BirthDate,
    Guid CitizenshipId,
    Guid CirculationTypeId,
    Guid ExpirationId,
    Guid OrganId,
    string Section,
    string Official,
    List<string> PhoneNumbers,
    string Instruction,
    string Observations,
    string ReasonForCirculation,
    string SecondName = null!,
    string LastName2 = null!) : ICommand;

internal sealed class UpdateCirculationCommandHandler(IRepositoryAsync<Circulation> repository, IMapper mapper)
    : ICommandHandler<UpdateCirculationCommand>
{
    public async Task<Result> Handle(UpdateCirculationCommand command, CancellationToken cancellationToken)
    {
        var circulation = await repository.GetByIdAsync(command.CirculationId, cancellationToken);

        if (circulation == null)
            return Result.Failure(DomainErrors.Circulation.NotFound);

        mapper.Map(command, circulation);

        await repository.UpdateAsync(circulation, cancellationToken);
            
        return Result.Success();
    }
}