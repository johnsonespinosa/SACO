using Newtonsoft.Json;

namespace Application.DTOs.Users;

public class AuthenticationResponse
{
    public string? UserId { get; init; }
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public List<string>? Roles { get; init; }
    public string? JwtToken { get; init; }
    public bool IsVerified { get; init; }

    [JsonIgnore]
    public string? RefreshJwtSecurityToken { get; init; }
}