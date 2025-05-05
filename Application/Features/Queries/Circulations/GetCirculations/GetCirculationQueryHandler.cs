using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations;
using Domain.Errors;
using Domain.Models;

namespace Application.Features.Queries.Circulations.GetCirculations;

internal sealed class GetCirculationQueryHandler(IRepositoryAsync<Circulation> repository, IMapper mapper)
    : IQueryHandler<GetCirculationQuery, PaginatedResult<CirculationDto>>
{
    public async Task<Result<PaginatedResult<CirculationDto>>> Handle(GetCirculationQuery request, CancellationToken cancellationToken)
    {
        if (request.PageNumber < 1)
            return Result.Failure<PaginatedResult<CirculationDto>>(DomainErrors.General.InvalidPageNumber);
        if (request.PageSize is < 1 or > 100)
            return Result.Failure<PaginatedResult<CirculationDto>>(DomainErrors.General.InvalidPageSize);
        
        var spec = new GetCirculationsSpecification(
            request.SearchTerm,
            request.PageSize,
            request.PageNumber);

        var total = await repository.CountAsync(spec, cancellationToken);
        var circulations = await repository.ListAsync(spec, cancellationToken);
        var items = mapper.Map<List<CirculationDto>>(circulations);
        
        return Result.Success(PaginatedResult<CirculationDto>.Create(
            items,
            total,
            request.PageNumber,
            request.PageSize));
    }
}