using Application.Interfaces;
using Domain.Entities;
using Shared.DTOs;
using Shared.DTOs.Circulations;
using Shared.Interfaces;

namespace Application.Services;

public class CirculationTypeService(IRepositoryAsync<CirculationType> repositoryAsync, IMapper mapper) : ICirculationTypeService
{
    public async Task<ServiceResponse<IReadOnlyCollection<CirculationTypeResponse>>> GetAll()
    {
        var circulations = await repositoryAsync.ListAsync();
        var circulationsMapped = mapper.Map<List<CirculationTypeResponse>>(circulations);
        return new ServiceResponse<IReadOnlyCollection<CirculationTypeResponse>>(
            Result: circulationsMapped, Succeeded: true, Message: ResponseMessages.ReadSuccess);
    }
}