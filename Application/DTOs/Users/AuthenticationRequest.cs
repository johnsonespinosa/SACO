namespace Application.DTOs.Users;

public class AuthenticationRequest
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}