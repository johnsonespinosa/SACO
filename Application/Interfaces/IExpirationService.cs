using Application.DTOs.Expirations;

namespace Application.Interfaces;

public interface IExpirationService
{
    Task<IReadOnlyCollection<ExpirationResponse>> GetAll();
}