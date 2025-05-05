namespace Application.Abstractions.Interfaces.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
}