using System.Net.Http.Json;
using Shared.DTOs;
using Shared.DTOs.Circulations;
using Shared.Interfaces;

namespace Client.Services;

public class CirculationTypeService(HttpClient httpClient) : ICirculationTypeService
{
    public async Task<ServiceResponse<IReadOnlyCollection<CirculationTypeResponse>>> GetAll()
    {
        return (await httpClient.GetFromJsonAsync<ServiceResponse<IReadOnlyCollection<CirculationTypeResponse>>>(requestUri: "/api/Circulations/"))!;
    }
}