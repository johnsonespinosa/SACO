using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;

namespace Application.Features.Queries.Circulations.GetCirculationById;

public record GetCirculationByIdQuery(Guid CirculationId) : IQuery<CirculationDto>;

