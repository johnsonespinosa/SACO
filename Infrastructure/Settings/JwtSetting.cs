using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Settings;

public sealed class JwtSetting 
{
    [Required(ErrorMessage = "JWT Key es requerida")]
    [StringLength(512, MinimumLength = 32, ErrorMessage = "La clave debe tener entre 32 y 512 caracteres")]
    public string Key { get; init; } = string.Empty;

    [Required(ErrorMessage = "JWT Issuer es requerido")]
    public string Issuer { get; init; } = string.Empty;

    [Required(ErrorMessage = "JWT Audience es requerida")]
    public string Audience { get; init; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Duración debe ser al menos 1 minuto")]
    public int AccessTokenDurationMinutes { get; init; }
    
    [Range(1, 365, ErrorMessage = "Duración refresh debe ser entre 1 y 365 días")]
    public int RefreshTokenDurationDays { get; init; }
}