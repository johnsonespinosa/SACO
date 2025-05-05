using System.Net.Http.Json;
using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Services;
using Domain.Errors;
using Domain.Models;

namespace WebApp.Services;

public class CirculationService(HttpClient httpClient) : ICirculationService
{
    public async Task<Result> CreateAsync(CirculationDto dto)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri: "api/circulations/", value: dto);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result>();
        return result!;
    }

    public async Task<Result> UpdateAsync(CirculationDto dto)
    {
        var response = await httpClient.PutAsJsonAsync(requestUri: $"api/circulations/{dto.CirculationId}",  value: dto);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result>();
        return result!;
    }

    public async Task<Result> DeleteAsync(Guid userId)
    {
        var response = await httpClient.DeleteAsync(requestUri: $"api/circulations/{userId}");
        return await HandleResponse<Result>(response);
    }
    
    private async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>();
            return Result.Failure<T>(error!);
        }
    
        return Result.Success<T>(await response.Content.ReadFromJsonAsync<T>() ?? default!);
    }

    public async Task<Result<PaginatedResult<CirculationDto>>> GetAllAsync(PaginationDto paginationDto)
    {
        var query = $"?page={paginationDto.PageNumber}&pageSize={paginationDto.PageSize}&filter={Uri.EscapeDataString(paginationDto.Filter!)}";
        return (await httpClient.GetFromJsonAsync<Result<PaginatedResult<CirculationDto>>>(requestUri: $"api/circulations{query}"))!;
    }

    public async Task<Result<CirculationDto>> GetById(Guid userId)
    {
        return await httpClient.GetFromJsonAsync<Result<CirculationDto>>(requestUri: $"api/circulations/{userId}") ?? default!;
    }
}