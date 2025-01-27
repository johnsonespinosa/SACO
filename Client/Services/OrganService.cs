using System.Net.Http.Json;
using Shared.DTOs;
using Shared.DTOs.Organs;
using Shared.Interfaces;

namespace Client.Services;

public class OrganService(HttpClient httpClient) : IOrganService
{
    public async Task<ServiceResponse<IReadOnlyCollection<OrganResponse>>> GetAll()
    {
        return (await httpClient.GetFromJsonAsync<ServiceResponse<IReadOnlyCollection<OrganResponse>>>(requestUri: "/api/Organs/"))!;
    }
}