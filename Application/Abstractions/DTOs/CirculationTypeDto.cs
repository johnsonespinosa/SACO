namespace Application.Abstractions.DTOs;

public class CirculationTypeDto
{
    public Guid CirculationTypeId { get; init; }
    public string Abbreviation { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}