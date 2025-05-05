using Application.Abstractions.Interfaces.Services;

namespace Infrastructure.Services;

internal sealed class DateTimeService : IDateTimeService
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}