using Shared.DTOs;
using Shared.DTOs.Circulations;

namespace Shared.Interfaces;

public interface ICirculationTypeService
{
    Task<ServiceResponse<IReadOnlyCollection<CirculationTypeResponse>>> GetAll();
}