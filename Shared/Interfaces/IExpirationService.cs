using Shared.DTOs;
using Shared.DTOs.Expirations;

namespace Shared.Interfaces;

public interface IExpirationService
{
    Task<ServiceResponse<IReadOnlyCollection<ExpirationResponse>>> GetAll();
}