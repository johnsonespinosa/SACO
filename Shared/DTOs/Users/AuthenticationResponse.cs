using System.Text.Json.Serialization;

namespace Shared.DTOs.Users;

/// <summary>
/// Representa la respuesta de autenticación del usuario.
/// </summary>
public class AuthenticationResponse
{
    /// <summary>
    /// Obtiene el identificador único del usuario.
    /// </summary>
    public string? UserId { get; init; }
    
    /// <summary>
    /// Obtiene el nombre del usuario.
    /// </summary>
    public string? UserName { get; init; }
    
    /// <summary>
    /// Obtiene el correo electrónico del usuario.
    /// </summary>
    public string? Email { get; init; }
    
    /// <summary>
    /// Obtiene los roles asignados al usuario.
    /// </summary>
    public List<string>? Roles { get; init; }
    
    /// <summary>
    /// Obtiene el token JWT del usuario.
    /// </summary>
    public string? JwtToken { get; init; }
    
    /// <summary>
    /// Indica si el usuario ha verificado su cuenta.
    /// </summary>
    public bool IsVerified { get; init; }
    
    /// <summary>
    /// Obtiene el token de seguridad JWT para refrescar la sesión.
    /// Este campo se ignora durante la serialización a JSON.
    /// </summary>
    [JsonIgnore]
    public string? RefreshJwtSecurityToken { get; init; }
}