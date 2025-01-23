namespace Domain.Entities;

public sealed class Organ
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}