namespace Domain.Circulations.Nomenclatures;

public sealed class Expiration
{
    public Guid ExpirationId { get; init; }
    public string Description { get; init; } = string.Empty;
    public IReadOnlyCollection<Circulation> Circulations { get; init; } = new HashSet<Circulation>();
}