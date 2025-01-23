using Application.DTOs.Citizenships;
using Application.Interfaces;

namespace Application.UseCases.Citizenships.Queries.GetAll;

public record GetAllCitizenshipsQuery : IRequest<IReadOnlyCollection<CitizenshipResponse>>
{
    internal sealed class GetAllCitizenshipsQueryHandler(ICitizenshipService service) : IRequestHandler<GetAllCitizenshipsQuery, IReadOnlyCollection<CitizenshipResponse>>
    {
        public async Task<IReadOnlyCollection<CitizenshipResponse>> Handle(GetAllCitizenshipsQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll();
        }
    }
}