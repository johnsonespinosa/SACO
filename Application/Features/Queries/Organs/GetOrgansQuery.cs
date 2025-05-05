using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations.Nomenclatures;
using Domain.Models;

namespace Application.Features.Queries.Organs;

public record GetOrgansQuery : IQuery<IReadOnlyCollection<OrganDto>>;

internal sealed class GetOrgansQueryHandler(IRepositoryAsync<Organ> repository, IMapper mapper)
    : IQueryHandler<GetOrgansQuery, IReadOnlyCollection<OrganDto>>
{
    public async Task<Result<IReadOnlyCollection<OrganDto>>> Handle(GetOrgansQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.ListAsync(cancellationToken);
        var organs = mapper.Map<IReadOnlyCollection<OrganDto>>(entities);
        return Result.Success(organs);
    }
}