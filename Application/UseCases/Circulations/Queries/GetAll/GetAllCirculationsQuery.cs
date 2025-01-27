using Shared.DTOs;
using Shared.DTOs.Circulations;
using Shared.Interfaces;

namespace Application.UseCases.Circulations.Queries.GetAll;

public record GetAllCirculationsQuery : IRequest<ServiceResponse<IReadOnlyCollection<CirculationTypeResponse>>>
{
    internal sealed class GetAllCirculationsQueryHandler(ICirculationTypeService typeService) : IRequestHandler<GetAllCirculationsQuery, ServiceResponse<IReadOnlyCollection<CirculationTypeResponse>>>
    {
        public async Task<ServiceResponse<IReadOnlyCollection<CirculationTypeResponse>>> Handle(GetAllCirculationsQuery request, CancellationToken cancellationToken)
        {
            return await typeService.GetAll();
        }
    }
}
