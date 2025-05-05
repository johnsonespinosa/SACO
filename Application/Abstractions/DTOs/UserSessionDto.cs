namespace Application.Abstractions.DTOs;

public record UserSessionDto(string UserName, DateTimeOffset LastLogin, AuthResponse Tokens);