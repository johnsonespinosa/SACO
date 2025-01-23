using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class MenuRole
{
    public Guid Id { get; init; }
    public Guid MenuId { get; init; }
    public Menu? Menu { get; init; }
    public Guid RoleId { get; set; }
    public IdentityRole? Role { get; init; }
    public bool IsActive { get; init; }
}