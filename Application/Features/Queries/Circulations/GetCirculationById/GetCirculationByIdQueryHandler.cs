using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations;
using Domain.Models;

namespace Application.Features.Queries.Circulations.GetCirculationById;

internal sealed class GetCirculationByIdQueryHandler(IRepositoryAsync<Circulation> repository, IMapper mapper)
    : IQueryHandler<GetCirculationByIdQuery, CirculationDto>
{
    public async Task<Result<CirculationDto>> Handle(GetCirculationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.CirculationId, cancellationToken);
        var circulation = mapper.Map<CirculationDto>(entity);
        return Result.Success(circulation);
    }
}