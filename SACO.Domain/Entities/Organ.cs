using SACO.Domain.Common;

namespace SACO.Domain.Entities;

public class Organ : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<Department> Departments { get; set; } = new List<Department>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Circulation> Circulations { get; set; } = new List<Circulation>();
}