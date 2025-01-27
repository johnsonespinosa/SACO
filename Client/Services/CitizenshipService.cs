using System.Net.Http.Json;
using Shared.DTOs;
using Shared.DTOs.Citizenships;
using Shared.Interfaces;

namespace Client.Services;

public class CitizenshipService(HttpClient httpClient) : ICitizenshipService
{
    public async Task<ServiceResponse<IReadOnlyCollection<CitizenshipResponse>>> GetAll()
    {
        return (await httpClient.GetFromJsonAsync<ServiceResponse<IReadOnlyCollection<CitizenshipResponse>>>(requestUri: "/api/Citizenships/"))!;
    }
}