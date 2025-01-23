namespace Domain.Entities;

public sealed class Citizenship
{
    public required string Code { get; init; }
    public decimal NumericCode { get; init; }
    public required string Description { get; init; }
}