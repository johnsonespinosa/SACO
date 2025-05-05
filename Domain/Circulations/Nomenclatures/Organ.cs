namespace Domain.Circulations.Nomenclatures;

public sealed class Organ
{
    public Guid OrganId { get; init; }
    public string Name { get; init; } = string.Empty;
    public IReadOnlyCollection<Circulation> Circulations { get; init; } = new HashSet<Circulation>();
}