using SACO.Domain.Enums;

namespace SACO.Shared.Models;

public class CirculationResponse
{
    public Guid Id { get; set; }
    public string ExpeditionNumber { get; set; } = string.Empty;
    public CirculationType Type { get; set; }
    public CirculationStatus Status { get; set; }
    public DateTime CirculationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string? Instruction { get; set; }
    public string? CirculationReason { get; set; }
    public string? Session { get; set; }
    public string? OperatingOfficer { get; set; }
    public string? OfficerPhone { get; set; }
    public string? IirOfficer { get; set; }
    
    // Passenger Information
    public Guid PassengerId { get; set; }
    public string PassengerFullName { get; set; } = string.Empty;
    public string PassengerCitizenship { get; set; } = string.Empty;
    public DateTime PassengerBirthDate { get; set; }
    
    // Information from the agency and department
    public string OrganName { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    
    // Audit information
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string? ValidatedBy { get; set; }
    public DateTime? ValidatedAt { get; set; }
}