using SACO.Domain.Common;
using SACO.Domain.Enums;

namespace SACO.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    
    // Foreign keys
    public Guid DepartmentId { get; set; }
    public Guid OrganId { get; set; }
    
    // Navigation properties
    public Department Department { get; set; } = null!;
    public Organ Organ { get; set; } = null!;
    public ICollection<Circulation> CreatedCirculations { get; set; } = new List<Circulation>();
    public ICollection<Circulation> ValidatedCirculations { get; set; } = new List<Circulation>();
    
    // Computed property
    public string FullName => $"{FirstName} {LastName}";
}