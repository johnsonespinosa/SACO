using System.Net.Http.Json;
using Shared.DTOs;
using Shared.DTOs.Citizenships;
using Shared.DTOs.Expirations;
using Shared.Interfaces;

namespace Client.Services;

public class ExpirationService(HttpClient httpClient) : IExpirationService
{
    public async Task<ServiceResponse<IReadOnlyCollection<ExpirationResponse>>> GetAll()
    {
        return (await httpClient.GetFromJsonAsync<ServiceResponse<IReadOnlyCollection<ExpirationResponse>>>(requestUri: "/api/Expirations/"))!;
    }
}