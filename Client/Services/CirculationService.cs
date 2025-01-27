using ErrorOr;
using MediatR;
using Shared.DTOs.OperationalCirculations;
using Shared.Interfaces;
using System.Net.Http.Json;
using Shared.DTOs;

namespace Client.Services;

public class CirculationService(HttpClient httpClient) : ICirculationService
{
    public async Task<ServiceResponse<ErrorOr<Unit>>> CreateAsync(CirculationRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri: "/api/OperationalCirculations/Create", value: request);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<ErrorOr<Unit>>>();
        return result!;
    }

    public async Task<ServiceResponse<ErrorOr<Unit>>> Update(CirculationRequest request)
    {
        var response = await httpClient.PutAsJsonAsync(requestUri: "/api/OperationalCirculations/Update",  value: request);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<ErrorOr<Unit>>>();
        return result!;
    }

    public async Task<ServiceResponse<ErrorOr<Unit>>> Delete(Guid id)
    {
        return (await httpClient.DeleteFromJsonAsync<ServiceResponse<ErrorOr<Unit>>>(requestUri: $"/api/Users/Delete/{id}"))!;
    }

    public async Task<ServiceResponse<IReadOnlyCollection<CirculationResponse>>> GetAll(string filterRequest)
    {
        return (await httpClient.GetFromJsonAsync<ServiceResponse<IReadOnlyCollection<CirculationResponse>>>(requestUri: "/api/OperationalCirculations/"))!;
    }
}