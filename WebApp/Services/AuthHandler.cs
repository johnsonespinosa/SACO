using System.Net;
using System.Net.Http.Headers;
using Application.Abstractions.Interfaces.Services;

namespace WebApp.Services;

public class AuthHandler(
    ITokenManager tokenManager, 
    IAuthService authService,
    ILogger<AuthHandler> logger) 
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        // Adjuntar token si existe
        var token = await tokenManager.GetAccessToken();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Manejar respuesta no autorizada
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            try
            {
                logger.LogInformation("Intento de refrescar token debido a 401");
                var refreshResult = await authService.RefreshToken();
                
                if (refreshResult.IsSuccess)
                {
                    // Reintentar con nuevo token
                    token = await tokenManager.GetAccessToken();
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    return await base.SendAsync(request, cancellationToken);
                }
                
                logger.LogWarning("Falló el refresh token, cerrando sesión");
                await authService.Logout();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al manejar respuesta 401");
                await authService.Logout();
            }
        }

        return response;
    }
}