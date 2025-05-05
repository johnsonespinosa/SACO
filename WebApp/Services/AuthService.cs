using System.Net.Http.Json;
using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Services;
using Domain.Errors;
using Domain.Models;
using WebApp.Utilities;

namespace WebApp.Services;

public class AuthService(
    HttpClient httpClient,
    ITokenManager tokenManager,
    ISessionStorageService sessionStorage,
    ILogger<AuthService> logger)
    : IAuthService
{
    private const string UserSessionKey = "user_session";
    private const string AuthEndpoint = "api/auth";
    private const string RefreshTokenEndpoint = "api/auth/refresh-token";

    public async Task<Result<AuthResponse>> Authenticate(AuthRequest request)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync(AuthEndpoint, request);
            
            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("Autenticación fallida para usuario: {UserHash}", HashUtility.Obfuscate(request.UserName));
                return Result.Failure<AuthResponse>(Error.Unauthorized(
                    code: "Auth.InvalidCredentials",
                    title: "Autenticación fallida",
                    detail: "Credenciales inválidas"));
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            
            if (authResponse == null)
            {
                logger.LogWarning("Respuesta de autenticación nula");
                return Result.Failure<AuthResponse>(Error.Unauthorized(
                    code: "Auth.NullResponse",
                    title: "Error de servidor",
                    detail: "Respuesta inválida del servidor"));
            }

            await tokenManager.SetTokens(authResponse.AccessToken, authResponse.RefreshToken);
            await sessionStorage.SetItemAsync(UserSessionKey, new UserSessionDto(
                request.UserName,
                DateTimeOffset.UtcNow,
                authResponse));
            
            logger.LogInformation("Autenticación exitosa para usuario: {UserHash}", HashUtility.Obfuscate(request.UserName));
            return Result.Success(authResponse);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error durante la autenticación");
            return Result.Failure<AuthResponse>(Error.Internal(
                code: "Auth.AuthenticationError",
                title: "Error de sistema",
                detail: "Error interno durante la autenticación"));
        }
    }

    public async Task Logout()
    {
        try
        {
            await tokenManager.ClearTokens();
            await sessionStorage.RemoveItemAsync(UserSessionKey);
            logger.LogInformation("Sesión cerrada");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al cerrar sesión");
        }
    }

    public async Task<Result<AuthResponse>> RefreshToken()
    {
        try
        {
            var refreshToken = await tokenManager.GetRefreshToken();
            if (string.IsNullOrEmpty(refreshToken))
            {
                logger.LogWarning("Intento de refresh sin token existente");
                return Result.Failure<AuthResponse>(Error.Unauthorized(
                    code: "Auth.MissingRefreshToken",
                    title: "Sesión expirada",
                    detail: "Por favor inicie sesión nuevamente"));
            }

            var accessToken = await tokenManager.GetAccessToken();
            var request = new RefreshTokenRequest
            {
                AccessToken = accessToken ?? string.Empty,
                RefreshToken = refreshToken
            };

            var response = await httpClient.PostAsJsonAsync(RefreshTokenEndpoint, request);
            
            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("Error al refrescar token: {StatusCode}", response.StatusCode);
                return Result.Failure<AuthResponse>(Error.Unauthorized(
                    code: "Auth.RefreshFailed",
                    title: "Sesión expirada",
                    detail: "La sesión no pudo ser renovada"));
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (authResponse == null)
            {
                logger.LogError("Respuesta de refresh token nula");
                return Result.Failure<AuthResponse>(Error.Internal(
                    code: "Auth.RefreshError",
                    title: "Error de sistema",
                    detail: "Error interno al renovar sesión"));
            }

            await tokenManager.SetTokens(authResponse.AccessToken, authResponse.RefreshToken);
            
            var currentSession = await sessionStorage.GetItemAsync<UserSessionDto>(UserSessionKey);
            var updatedSession = currentSession != null
                ? currentSession with { Tokens = authResponse }
                : new UserSessionDto("Usuario", DateTimeOffset.UtcNow, authResponse);
            
            await sessionStorage.SetItemAsync(UserSessionKey, updatedSession);
            
            return Result.Success(authResponse);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error crítico al refrescar token");
            return Result.Failure<AuthResponse>(Error.Internal(
                code: "Auth.RefreshTokenError",
                title: "Error de sistema",
                detail: "Error interno al renovar sesión"));
        }
    }

    public async Task<Result<UserSessionDto>> GetUserSession()
    {
        try
        {
            var session = await sessionStorage.GetItemAsync<UserSessionDto>(UserSessionKey);
            if (session == null) return Result.Failure<UserSessionDto>(Error.NotFound(
                code: "Auth.SessionNotFound",
                title: "Sesión no encontrada",
                detail: "No hay sesión activa"));

            var tokenValidation = await tokenManager.ValidateTokenAsync(session.Tokens.AccessToken);
            return tokenValidation.IsValid
                ? Result.Success(session)
                : Result.Failure<UserSessionDto>(Error.Unauthorized(
                    code: "Auth.InvalidSession",
                    title: "Sesión inválida",
                    detail: "La sesión actual no es válida"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al obtener sesión");
            return Result.Failure<UserSessionDto>(Error.Internal(
                code: "Auth.SessionError",
                title: "Error de sistema",
                detail: "Error al recuperar la sesión"));
        }
    }
}