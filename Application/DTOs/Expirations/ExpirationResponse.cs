namespace Application.DTOs.Expirations;

public class ExpirationResponse
{
    public Guid Id { get; init; }
    public required string Description { get; init; }
}