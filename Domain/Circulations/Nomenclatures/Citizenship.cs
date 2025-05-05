namespace Domain.Circulations.Nomenclatures;

public sealed class Citizenship
{
    public Guid CitizenshipId { get; init; }
    public string Abbreviation { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IReadOnlyCollection<Circulation> Circulations { get; init; } = new HashSet<Circulation>();
}