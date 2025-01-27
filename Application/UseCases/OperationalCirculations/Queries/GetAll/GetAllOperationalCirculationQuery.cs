using Shared.DTOs;
using Shared.DTOs.OperationalCirculations;
using Shared.Interfaces;

namespace Application.UseCases.OperationalCirculations.Queries.GetAll;

public record GetAllOperationalCirculationQuery(string FilterText) : IRequest<ServiceResponse<IReadOnlyCollection<CirculationResponse>>>
{
    internal sealed class GetAllOperationalCirculationQueryHandler(ICirculationService service) : IRequestHandler<GetAllOperationalCirculationQuery, ServiceResponse<IReadOnlyCollection<CirculationResponse>>>
    {
        public async Task<ServiceResponse<IReadOnlyCollection<CirculationResponse>>> Handle(GetAllOperationalCirculationQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll(request.FilterText);
        }
    }
}