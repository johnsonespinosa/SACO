namespace Application.Abstractions.DTOs;

public class ExpirationDto
{
    public Guid ExpirationId { get; init; }
    public string Description { get; init; } = string.Empty;
}