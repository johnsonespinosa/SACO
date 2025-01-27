using Application.Interfaces;
using Domain.Entities;
using Shared.DTOs;
using Shared.DTOs.Citizenships;
using Shared.Interfaces;

namespace Application.Services;

public class CitizenshipService(IRepositoryAsync<Citizenship> repositoryAsync, IMapper mapper) : ICitizenshipService
{
    public async Task<ServiceResponse<IReadOnlyCollection<CitizenshipResponse>>> GetAll()
    {
        var citizenships = await repositoryAsync.ListAsync();
        var citizenshipsMapped = mapper.Map<List<CitizenshipResponse>>(citizenships);
        return new ServiceResponse<IReadOnlyCollection<CitizenshipResponse>>(
            Result: citizenshipsMapped, Succeeded: true, Message: ResponseMessages.ReadSuccess);
    }
}