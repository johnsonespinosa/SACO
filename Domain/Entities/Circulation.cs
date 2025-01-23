namespace Domain.Entities;

public sealed class Circulation
{
    public Guid Id { get; init; }
    public required string Code { get; init; }

    public required string Description { get; init; }
}