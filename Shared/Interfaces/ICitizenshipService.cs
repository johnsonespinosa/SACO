using Shared.DTOs;
using Shared.DTOs.Citizenships;

namespace Shared.Interfaces;

public interface ICitizenshipService
{
    Task<ServiceResponse<IReadOnlyCollection<CitizenshipResponse>>> GetAll();
}