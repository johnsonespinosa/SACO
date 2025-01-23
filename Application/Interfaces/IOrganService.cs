using Application.DTOs.Organs;

namespace Application.Interfaces;

public interface IOrganService
{
    Task<IReadOnlyCollection<OrganResponse>> GetAll();
}