using SACO.Domain.Common;

namespace SACO.Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    
    // Foreign keys
    public Guid OrganId { get; set; }
    
    // Navigation properties
    public Organ Organ { get; set; } = null!;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Circulation> Circulations { get; set; } = new List<Circulation>();
}