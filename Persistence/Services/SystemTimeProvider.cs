using Application.Interfaces;

namespace Infrastructure.Services;

public class SystemTimeProvider : ITimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}