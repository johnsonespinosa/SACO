using Microsoft.AspNetCore.Identity;
using SACO.Domain.Enums;

namespace SACO.Domain.Entities;

public class User: IdentityUser<Guid>
{
    // Additional properties specific to our domain
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    // Specific roles of the SACO system
    public UserType UserType { get; set; }
    
    // Foreign keys
    public Guid DepartmentId { get; set; }
    public Guid OrganId { get; set; }
    
    // Navigation properties
    public Department Department { get; set; } = null!;
    public Organ Organ { get; set; } = null!;
    
    // Additional audit properties
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    
    // Computed property
    public string FullName => $"{FirstName} {LastName}";
}