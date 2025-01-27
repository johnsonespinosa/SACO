namespace Shared.DTOs.Circulations;

public class CirculationTypeResponse
{
    public Guid Id { get; set; }
    public string Abbreviation { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}