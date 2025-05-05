using System.Net.Http.Json;
using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Services;
using Domain.Models;

namespace WebApp.Services;

public class OrganService(HttpClient httpClient) : IOrganService
{
    public async Task<Result<IReadOnlyCollection<OrganDto>>> GetAllAsync()
    {
        return (await httpClient.GetFromJsonAsync<Result<IReadOnlyCollection<OrganDto>>>(requestUri: "api/organs"))!;
    }
}