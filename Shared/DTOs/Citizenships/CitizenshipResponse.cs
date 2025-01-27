namespace Shared.DTOs.Citizenships;

public class CitizenshipResponse
{
    public Guid Id { get; init; }
    public string Abbreviation { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}