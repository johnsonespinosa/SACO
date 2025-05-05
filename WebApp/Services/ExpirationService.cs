using System.Net.Http.Json;
using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Services;
using Domain.Models;

namespace WebApp.Services;

public class ExpirationService(HttpClient httpClient) : IExpirationService
{
    public async Task<Result<IReadOnlyCollection<ExpirationDto>>> GetAllAsync()
    {
        return (await httpClient.GetFromJsonAsync<Result<IReadOnlyCollection<ExpirationDto>>>(requestUri: "api/expirations"))!;
    }
}