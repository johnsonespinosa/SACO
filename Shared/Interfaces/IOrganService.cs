using Shared.DTOs;
using Shared.DTOs.Organs;

namespace Shared.Interfaces;

public interface IOrganService
{
    Task<ServiceResponse<IReadOnlyCollection<OrganResponse>>> GetAll();
}