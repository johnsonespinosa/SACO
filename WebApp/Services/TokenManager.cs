using System.IdentityModel.Tokens.Jwt;
using Application.Abstractions.Interfaces.Services;
using Domain.Models;

namespace WebApp.Services;

public class TokenManager(
    ISessionStorageService sessionStorage,
    ILogger<TokenManager> logger) : ITokenManager
{
    private const string AccessTokenKey = "auth_token";
    private const string RefreshTokenKey = "refresh_token";

    public async Task<string?> GetAccessToken() => 
        await GetTokenSafe(AccessTokenKey);

    public async Task<string?> GetRefreshToken() => 
        await GetTokenSafe(RefreshTokenKey);

    public async Task SetTokens(string accessToken, string refreshToken)
    {
        await sessionStorage.SetItemAsync(AccessTokenKey, accessToken);
        await sessionStorage.SetItemAsync(RefreshTokenKey, refreshToken);
    }

    public async Task ClearTokens()
    {
        await sessionStorage.RemoveItemAsync(AccessTokenKey);
        await sessionStorage.RemoveItemAsync(RefreshTokenKey);
    }

    public async Task<TokenValidationResult> ValidateTokenAsync(string? token)
    {
        if (string.IsNullOrEmpty(token))
            return new TokenValidationResult { IsValid = false, Error = "Token vacío" };

        try
        {
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
                return new TokenValidationResult { IsValid = false, Error = "Token inválido" };

            var jwtToken = handler.ReadJwtToken(token);
            var isExpired = jwtToken.ValidTo < DateTime.UtcNow.AddMinutes(1);
            
            return new TokenValidationResult
            {
                IsValid = !isExpired,
                Expiration = jwtToken.ValidTo,
                Error = isExpired ? "Token expirado" : null
            };
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error validando token");
            return new TokenValidationResult 
            { 
                IsValid = false, 
                Error = "Error de validación: " + ex.Message 
            };
        }
    }

    private async Task<string?> GetTokenSafe(string key)
    {
        try
        {
            return await sessionStorage.GetItemAsync<string>(key);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al obtener token: {Key}", key);
            await sessionStorage.RemoveItemAsync(key);
            return null;
        }
    }
}

