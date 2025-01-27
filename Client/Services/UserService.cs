using ErrorOr;
using MediatR;
using Shared.DTOs.Users;
using Shared.Interfaces;
using System.Net.Http.Json;
using Shared.DTOs;

namespace Client.Services;

/// <summary>
/// Servicio para gestionar las operaciones relacionadas con los usuarios.
/// </summary>
public class UserService(HttpClient httpClient) : IUserService
{
    /// <summary>
    /// Autentica a un usuario.
    /// </summary>
    /// <param name="request">Los datos necesarios para la autenticación.</param>
    /// <param name="ipAddress">La dirección IP del cliente.</param>
    /// <returns>Una respuesta con el resultado de la autenticación.</returns>
    public async Task<ServiceResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri: "/api/Users/Authenticate", request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<AuthenticationResponse>>();
        return result!;
    }

    /// <summary>
    /// Agrega un nuevo usuario.
    /// </summary>
    /// <param name="request">Los datos necesarios para crear un usuario.</param>
    /// <returns>El resultado de la creación del usuario.</returns>
    public async Task<ServiceResponse<ErrorOr<Unit>>> AddAsync(UserRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(requestUri: "/api/Users/Create", value: request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<ErrorOr<Unit>>>();
        return result!;
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="request">Los datos necesarios para actualizar el usuario.</param>
    /// <returns>El resultado de la actualización del usuario.</returns>
    public async Task<ServiceResponse<ErrorOr<Unit>>> UpdateAsync(UserRequest request)
    {
        var response = await httpClient.PutAsJsonAsync(requestUri: "/api/Users/Update",  value: request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<ErrorOr<Unit>>>();
        return result!;
    }

    /// <summary>
    /// Elimina un usuario por ID.
    /// </summary>
    /// <param name="userId">El identificador del usuario a eliminar.</param>
    /// <returns>El resultado de la eliminación del usuario.</returns>
    public async Task<ServiceResponse<ErrorOr<Unit>>> DeleteAsync(string userId)
    {
        return (await httpClient.DeleteFromJsonAsync<ServiceResponse<ErrorOr<Unit>>>(requestUri: $"/api/Users/Delete/{userId}"))!;
    }

    /// <summary>
    /// Obtiene todos los usuarios, aplicando un filtro opcional.
    /// </summary>
    /// <param name="filterRequest">Cadena de filtro para limitar los resultados (opcional).</param>
    /// <returns>Una respuesta que contiene una lista de usuarios.</returns>
    public async Task<ServiceResponse<IReadOnlyCollection<UserResponse>>> GetAllAsync(string? filterRequest = null)
    {
        // Construir la URI con el filtro si se proporciona
        var requestUri = string.IsNullOrEmpty(filterRequest) 
            ? "/api/Users/" 
            : $"/api/Users?filter={Uri.EscapeDataString(filterRequest)}"; // Asegúrate de codificar la cadena de consulta

        // Realiza la solicitud GET y deserializa la respuesta
        var response = await httpClient.GetFromJsonAsync<ServiceResponse<IReadOnlyCollection<UserResponse>>>(requestUri);

        return response!;
    }

    public async Task<ServiceResponse<UserResponse>> GetById(string id)
    {
        return (await httpClient.GetFromJsonAsync<ServiceResponse<UserResponse>>(requestUri: $"/api/Persona/Obtener/{id}"))!;
    }
}