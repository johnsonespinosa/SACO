using Shared.DTOs;
using Shared.DTOs.Citizenships;
using Shared.Interfaces;

namespace Application.UseCases.Citizenships.Queries.GetAll;

public record GetAllCitizenshipsQuery : IRequest<ServiceResponse<IReadOnlyCollection<CitizenshipResponse>>>
{
    internal sealed class GetAllCitizenshipsQueryHandler(ICitizenshipService service) : IRequestHandler<GetAllCitizenshipsQuery, ServiceResponse<IReadOnlyCollection<CitizenshipResponse>>>
    {
        public async Task<ServiceResponse<IReadOnlyCollection<CitizenshipResponse>>> Handle(GetAllCitizenshipsQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll();
        }
    }
}