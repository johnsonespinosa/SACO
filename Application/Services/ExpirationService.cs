using Application.DTOs.Expirations;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ExpirationService(IRepositoryAsync<Expiration> repositoryAsync, IMapper mapper) : IExpirationService
{
    public async Task<IReadOnlyCollection<ExpirationResponse>> GetAll()
    {
        var expirations = await repositoryAsync.ListAsync();
        var expirationsMapped = mapper.Map<List<ExpirationResponse>>(expirations);
        return expirationsMapped;
    }
}