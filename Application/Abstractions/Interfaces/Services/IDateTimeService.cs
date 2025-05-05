namespace Application.Abstractions.Interfaces.Services;

public interface IDateTimeService
{
    public DateTimeOffset UtcNow { get; }
}