using Application.DTOs.Citizenships;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class CitizenshipService(IRepositoryAsync<Citizenship> repositoryAsync, IMapper mapper) : ICitizenshipService
{
    public async Task<IReadOnlyCollection<CitizenshipResponse>> GetAll()
    {
        var citizenships = await repositoryAsync.ListAsync();
        var citizenshipsMapped = mapper.Map<List<CitizenshipResponse>>(citizenships);
        return citizenshipsMapped;
    }
}