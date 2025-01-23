namespace Domain.Commons;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }
    public required string CreatedBy { get; set; }
    
    public DateTimeOffset LastModified { get; set; }
    public string LastModifiedBy { get; set; } = string.Empty;
    
    public DateTimeOffset Deleted { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
}