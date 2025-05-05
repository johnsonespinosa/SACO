using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations.Nomenclatures;
using Domain.Models;

namespace Application.Features.Queries.CirculationTypes;

public record GetCirculationTypesQuery : IQuery<IReadOnlyCollection<CirculationTypeDto>>;

internal sealed class GetCirculationTypesQueryHandler(IRepositoryAsync<CirculationType> repository, IMapper mapper)
    : IQueryHandler<GetCirculationTypesQuery, IReadOnlyCollection<CirculationTypeDto>>
{
    public async Task<Result<IReadOnlyCollection<CirculationTypeDto>>> Handle(GetCirculationTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.ListAsync(cancellationToken);
        var circulationTypes = mapper.Map<IReadOnlyCollection<CirculationTypeDto>>(entities);
        return Result.Success(circulationTypes);
    }
}