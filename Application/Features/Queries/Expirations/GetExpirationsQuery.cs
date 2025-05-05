using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations.Nomenclatures;
using Domain.Models;

namespace Application.Features.Queries.Expirations;

public record GetExpirationsQuery : IQuery<IReadOnlyCollection<ExpirationDto>>;

internal sealed class GetExpirationsQueryHandler(IRepositoryAsync<Expiration> repository, IMapper mapper)
    : IQueryHandler<GetExpirationsQuery, IReadOnlyCollection<ExpirationDto>>
{
    public async Task<Result<IReadOnlyCollection<ExpirationDto>>> Handle(GetExpirationsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.ListAsync(cancellationToken);
        var expirations = mapper.Map<IReadOnlyCollection<ExpirationDto>>(entities);
        return Result.Success(expirations);
    }
}