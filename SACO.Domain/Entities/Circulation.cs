using SACO.Domain.Common;
using SACO.Domain.Enums;

namespace SACO.Domain.Entities;

public class Circulation : BaseEntity
{
    // Basic circulation information
    public string ExpeditionNumber { get; set; } = string.Empty;
    public CirculationType Type { get; set; }
    public CirculationStatus Status { get; set; }
    public DateTime CirculationDate { get; set; } = DateTime.UtcNow;
    public DateTime ExpirationDate { get; set; }
    
    // Editable fields according to requirements
    public string? Instruction { get; set; }
    public string? CirculationReason { get; set; }
    public string? Session { get; set; }
    public string? OperatingOfficer { get; set; }
    public string? OfficerPhone { get; set; }
    public string? IirOfficer { get; set; }
    
    // Foreign keys
    public Guid PassengerId { get; set; }
    public Guid OrganId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid CreatorUserId { get; set; }
    public Guid? ValidatorUserId { get; set; }
    
    // Navigation properties
    public Passenger Passenger { get; set; } = null!;
    public Organ Organ { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public User CreatorUser { get; set; } = null!;
    public User? ValidatorUser { get; set; }
    
    // Search support fields
    public string TraceKey { get; private set; } = string.Empty;
    
    // Domain methods
    public void MarkAsEffective(Guid validatorUserId, string effectiveExpeditionNumber)
    {
        if (!CanBeValidated)
            throw new InvalidOperationException("Circulation cannot be validated");
            
        Status = CirculationStatus.Effective;
        ExpeditionNumber = effectiveExpeditionNumber;
        ValidatorUserId = validatorUserId;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = validatorUserId.ToString();
    }
    
    public void MarkAsRejected(Guid validatorUserId, string reason)
    {
        Status = CirculationStatus.Rejected;
        ValidatorUserId = validatorUserId;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = validatorUserId.ToString();
    }
    
    public void CheckExpiration()
    {
        if (IsExpired && Type is CirculationType.DG or CirculationType.DC)
        {
            Status = CirculationStatus.Expired;
        }
    }
    
    // Method to calculate TraceKey
    public void CalculateTraceKey()
    {
        TraceKey = $"{Passenger.FirstLastName}{Passenger.FirstName}{Passenger.BirthDate:yyyyMMdd}{Type}".ToLower();
    }
    
    // Computed properties (getters only - not persisted)
    public bool IsExpired => DateTime.UtcNow > ExpirationDate;
    public bool CanBeValidated => Status == CirculationStatus.Operative && !IsExpired;
    public bool IsEffective => Status == CirculationStatus.Effective;
    public bool IsOperative => Status == CirculationStatus.Operative;
    
    // Helper properties for ExpeditionNumber (not persisted)
    public string ExpeditionPrefix => ExpeditionNumber.Split('-').FirstOrDefault() ?? "";
    public int ExpeditionSequence 
    {
        get
        {
            var parts = ExpeditionNumber.Split('-');
            return parts.Length > 1 && int.TryParse(parts[1], out var sequence) ? sequence : 0;
        }
    }
}