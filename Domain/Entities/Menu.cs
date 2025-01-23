namespace Domain.Entities;

public sealed class Menu
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public Guid? ParentId { get; init; }
    public Menu? Parent { get; init; }
}