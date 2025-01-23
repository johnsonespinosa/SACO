using Application.DTOs.OperationalCirculations;
using Application.Interfaces;

namespace Application.UseCases.OperationalCirculations.Queries.GetAll;

public record GetAllOperationalCirculationQuery(string FilterText) : IRequest<IReadOnlyCollection<OperationalCirculationResponse>>
{
    internal sealed class GetAllOperationalCirculationQueryHandler(IOperationalCirculationService service) : IRequestHandler<GetAllOperationalCirculationQuery, IReadOnlyCollection<OperationalCirculationResponse>>
    {
        public async Task<IReadOnlyCollection<OperationalCirculationResponse>> Handle(GetAllOperationalCirculationQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll(request.FilterText);
        }
    }
}