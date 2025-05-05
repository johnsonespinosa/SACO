using System.Net.Http.Json;
using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Services;
using Domain.Models;

namespace WebApp.Services;

public class CitizenshipService(HttpClient httpClient) : ICitizenshipService
{
    public async Task<Result<IReadOnlyCollection<CitizenshipDto>>> GetAllAsync()
    {
        return (await httpClient.GetFromJsonAsync<Result<IReadOnlyCollection<CitizenshipDto>>>(requestUri: "api/citizenship"))!;
    }
}