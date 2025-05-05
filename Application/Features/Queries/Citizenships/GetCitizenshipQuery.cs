using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations.Nomenclatures;
using Domain.Models;

namespace Application.Features.Queries.Citizenships;

public record GetCitizenshipQuery : IQuery<IReadOnlyCollection<CitizenshipDto>>;

internal sealed class GetCitizenshipQueryHandler(IRepositoryAsync<Citizenship> repository, IMapper mapper)
    : IQueryHandler<GetCitizenshipQuery, IReadOnlyCollection<CitizenshipDto>>
{
    public async Task<Result<IReadOnlyCollection<CitizenshipDto>>> Handle(GetCitizenshipQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.ListAsync(cancellationToken);
        var citizenship = mapper.Map<IReadOnlyCollection<CitizenshipDto>>(entities);
        return Result.Success(citizenship);
    }
}