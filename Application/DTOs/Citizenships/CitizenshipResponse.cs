namespace Application.DTOs.Citizenships;

public class CitizenshipResponse
{
    public Guid Id { get; init; }
    public required string Abbreviation { get; init; }
    public required string Description { get; init; }
}