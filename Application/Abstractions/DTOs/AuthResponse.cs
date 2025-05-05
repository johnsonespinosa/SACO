namespace Application.Abstractions.DTOs;

public sealed record AuthResponse(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn);