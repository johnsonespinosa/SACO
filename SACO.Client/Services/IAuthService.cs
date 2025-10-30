using System.Net.Http.Json;
using SACO.Application.Models;

namespace SACO.Client.Services;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
    Task<AuthResponse> RegisterAsync(CreateUserRequest registerRequest);
    Task LogoutAsync();
    Task<CurrentUserResponse> GetCurrentUserAsync();
}

public class AuthService(HttpClient httpClient) : IAuthService
{
    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        var response = await httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
        return await response.Content.ReadFromJsonAsync<AuthResponse>() ?? new AuthResponse { Success = false };
    }

    public async Task<AuthResponse> RegisterAsync(CreateUserRequest registerRequest)
    {
        var response = await httpClient.PostAsJsonAsync("api/auth/register", registerRequest);
        return await response.Content.ReadFromJsonAsync<AuthResponse>() ?? new AuthResponse { Success = false };
    }

    public async Task LogoutAsync()
    {
        await httpClient.PostAsync("api/auth/logout", null);
    }

    public async Task<CurrentUserResponse> GetCurrentUserAsync()
    {
        return await httpClient.GetFromJsonAsync<CurrentUserResponse>("api/auth/current-user") ?? new CurrentUserResponse { IsAuthenticated = false };
    }
}