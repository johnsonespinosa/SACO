using Application.Interfaces;
using Domain.Entities;
using Shared.DTOs;
using Shared.DTOs.Organs;
using Shared.Interfaces;

namespace Application.Services;

public class OrganService(IRepositoryAsync<Organ> repositoryAsync, IMapper mapper) : IOrganService
{
    public async Task<ServiceResponse<IReadOnlyCollection<OrganResponse>>> GetAll()
    {
        var organs = await repositoryAsync.ListAsync();
        var organMapped = mapper.Map<List<OrganResponse>>(organs);
        return new ServiceResponse<IReadOnlyCollection<OrganResponse>>(
            Result: organMapped, Succeeded: true, Message: ResponseMessages.ReadSuccess);
    }
}