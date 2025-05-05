namespace Application.Abstractions.DTOs;

public record UserDto(
    string Id,
    string UserName,
    string Email,
    string PhoneNumber);