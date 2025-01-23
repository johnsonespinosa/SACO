using Application.DTOs.Circulations;
using Application.Interfaces;

namespace Application.UseCases.Circulations.Queries.GetAll;

public record GetAllCirculationsQuery : IRequest<IReadOnlyCollection<CirculationResponse>>
{
    internal sealed class GetAllCirculationsQueryHandler(ICirculationService service) : IRequestHandler<GetAllCirculationsQuery, IReadOnlyCollection<CirculationResponse>>
    {
        public async Task<IReadOnlyCollection<CirculationResponse>> Handle(GetAllCirculationsQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll();
        }
    }
}
