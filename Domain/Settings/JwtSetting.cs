using Microsoft.Extensions.Options;

namespace Domain.Settings;

public class JwtSetting 
{
    public string Key { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public double DurationInMinutes { get; init; }
}