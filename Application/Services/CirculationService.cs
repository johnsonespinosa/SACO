using Application.DTOs.Circulations;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class CirculationService(IRepositoryAsync<Circulation> repositoryAsync, IMapper mapper) : ICirculationService
{
    public async Task<IReadOnlyCollection<CirculationResponse>> GetAll()
    {
        var circulations = await repositoryAsync.ListAsync();
        var circulationsMapped = mapper.Map<List<CirculationResponse>>(circulations);
        return circulationsMapped;
    }
}