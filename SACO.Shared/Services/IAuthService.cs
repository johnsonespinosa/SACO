using SACO.Shared.Models;

namespace SACO.Shared.Services;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest loginRequest);
    Task<AuthResponse> RegisterAsync(CreateUserRequest registerRequest);
    Task LogoutAsync();
    Task<CurrentUserResponse> GetCurrentUserAsync();
}

