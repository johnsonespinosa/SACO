using Application.Interfaces;
using Domain.Entities;
using Shared.DTOs;
using Shared.DTOs.Expirations;
using Shared.Interfaces;

namespace Application.Services;

public class ExpirationService(IRepositoryAsync<Expiration> repositoryAsync, IMapper mapper) : IExpirationService
{
    public async Task<ServiceResponse<IReadOnlyCollection<ExpirationResponse>>> GetAll()
    {
        var expirations = await repositoryAsync.ListAsync();
        var expirationsMapped = mapper.Map<List<ExpirationResponse>>(expirations);
        return new ServiceResponse<IReadOnlyCollection<ExpirationResponse>>(
            Result: expirationsMapped, Succeeded: true, Message: ResponseMessages.ReadSuccess);
    }
}