using System.Net.Http.Json;
using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Services;
using Domain.Errors;
using Domain.Models;

namespace WebApp.Services;

public class UserService(HttpClient httpClient) : IUserService
{

    public async Task<Result> CreateAsync(UserCreateDto dto)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri: "api/users/", value: dto);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result>();
        return result!;
    }

    public async Task<Result> UpdateAsync(UserUpdateDto dto)
    {
        var response = await httpClient.PutAsJsonAsync(requestUri: $"api/users/{dto.Id}",  value: dto);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<Result>();
        return result!;
    }

    public async Task<Result> DeleteAsync(string userId)
    {
        var response = await httpClient.DeleteAsync(requestUri: $"api/users/{userId}");
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

    public async Task<Result<PaginatedResult<UserDto>>> GetAllAsync(PaginationDto paginationDto)
    {
        var query = $"?page={paginationDto.PageNumber}&pageSize={paginationDto.PageSize}&filter={Uri.EscapeDataString(paginationDto.Filter!)}";
        return (await httpClient.GetFromJsonAsync<Result<PaginatedResult<UserDto>>>(requestUri: $"api/users{query}"))!;
    }

    public async Task<Result<UserDto>> GetById(string userId)
    {
        return await httpClient.GetFromJsonAsync<Result<UserDto>>(requestUri: $"api/users/{userId}") ?? default!;
    }
}