
using Domain.Models;

namespace Application.Abstractions.Interfaces.Services;

public interface ITokenManager
{
    Task<string?> GetAccessToken();
    Task<string?> GetRefreshToken();
    Task SetTokens(string accessToken, string refreshToken);
    Task ClearTokens();
    Task<TokenValidationResult> ValidateTokenAsync(string? token); 
}