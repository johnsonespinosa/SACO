using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations;
using Domain.Models;

namespace Application.Features.Commands.Circulations.Create;

public record CreateCirculationCommand(
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
    string LastName2 = null!) : ICommand<Guid>;

internal sealed class CreateCirculationCommandHandler(
    IRepositoryAsync<Circulation> repository,
    IMapper mapper)
    : ICommandHandler<CreateCirculationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCirculationCommand request, CancellationToken cancellationToken)
    {
        var circulation = mapper.Map<Circulation>(request);
        await repository.AddAsync(circulation, cancellationToken);
        return Result.Success(circulation.CirculationId);
    }
}