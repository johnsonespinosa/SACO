using System.Net.Http.Json;
using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Services;
using Domain.Models;

namespace WebApp.Services;

public class CirculationTypeService(HttpClient httpClient) : ICirculationTypeService
{
    public async Task<Result<IReadOnlyCollection<CirculationTypeDto>>> GetAllAsync()
    {
        return (await httpClient.GetFromJsonAsync<Result<IReadOnlyCollection<CirculationTypeDto>>>(requestUri: "api/circulation-types"))!;
    }
}