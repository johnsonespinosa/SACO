using SACO.Domain.Enums;

namespace SACO.Shared.Models;

public class CreateCirculationRequest
{
    public CirculationType Type { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string? Instruction { get; set; }
    public string? CirculationReason { get; set; }
    public string? Session { get; set; }
    public string? OperatingOfficer { get; set; }
    public string? OfficerPhone { get; set; }
    public string? IirOfficer { get; set; }
    
    public Guid PassengerId { get; set; }
    public Guid OrganId { get; set; }
    public Guid DepartmentId { get; set; }
}