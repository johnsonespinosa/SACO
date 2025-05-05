namespace Application.Abstractions.DTOs;

public class CitizenshipDto
{
    public Guid CitizenshipId { get; init; }
    public string Abbreviation { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}