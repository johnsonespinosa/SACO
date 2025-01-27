namespace Domain.Entities;

public sealed class CirculationType
{
    public Guid Id { get; init; }
    public required string Abbreviation { get; init; }
    public required string Description { get; init; }
    public IEnumerable<Circulation>? Circulations { get; init; }
}