using Application.DTOs.Organs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class OrganService(IRepositoryAsync<Organ> repositoryAsync, IMapper mapper) : IOrganService
{
    public async Task<IReadOnlyCollection<OrganResponse>> GetAll()
    {
        var organs = await repositoryAsync.ListAsync();
        var organMapped = mapper.Map<List<OrganResponse>>(organs);
        return organMapped;
    }
}