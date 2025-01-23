namespace Application.DTOs.Circulations;

public class CirculationResponse
{
    public Guid Id { get; init; }
    public required string Abbreviation { get; init; }
    public required string Description { get; init; }
}