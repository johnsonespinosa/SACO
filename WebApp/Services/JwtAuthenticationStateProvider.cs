using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Abstractions.Interfaces.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApp.Services;

public class JwtAuthenticationStateProvider(
    ITokenManager tokenManager,
    IAuthService authService,
    ILogger<JwtAuthenticationStateProvider> logger)
    : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var sessionResult = await authService.GetUserSession();
            if (sessionResult.IsFailure)
                return CreateUnauthenticatedState();

            var token = await tokenManager.GetAccessToken();
            if (string.IsNullOrEmpty(token))
                return CreateUnauthenticatedState();

            var claims = ParseClaimsFromJwt(token);
            if (IsTokenExpired(claims))
            {
                logger.LogInformation("Token expirado, intentando refresh...");
                var refreshResult = await authService.RefreshToken();
                return refreshResult.IsSuccess 
                    ? await GetAuthenticationStateAsync() 
                    : CreateUnauthenticatedState();
            }

            var identity = new ClaimsIdentity(claims, "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error en estado de autenticación");
            return CreateUnauthenticatedState();
        }
    }

    private AuthenticationState CreateUnauthenticatedState() => 
        new(new ClaimsPrincipal(new ClaimsIdentity()));

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims;
        }
        catch
        {
            return [];
        }
    }

    private static bool IsTokenExpired(IEnumerable<Claim> claims)
    {
        var expClaim = claims.FirstOrDefault(c => c.Type == "exp");
        if (expClaim == null || !long.TryParse(expClaim.Value, out var expTimestamp))
            return true;

        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expTimestamp);
        return expirationTime < DateTimeOffset.UtcNow.AddMinutes(1);
    }

    public void NotifyAuthenticationStateChanged() => 
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
}