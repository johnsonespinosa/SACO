namespace Domain.Entities;

public sealed class Expiration
{
    public Guid Id { get; init; }
    public required string Description { get; init; }
}