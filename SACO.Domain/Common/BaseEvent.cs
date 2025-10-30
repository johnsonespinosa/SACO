namespace SACO.Domain.Common;

// This class will not be mapped by Entity Framework
// It is only for domain events that will be handled by MediatR or another mechanism
public abstract class BaseEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
    public bool IsPublished { get; set; }
}