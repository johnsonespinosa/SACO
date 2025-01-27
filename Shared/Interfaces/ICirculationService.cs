using ErrorOr;
using MediatR;
using Shared.DTOs;
using Shared.DTOs.OperationalCirculations;

namespace Shared.Interfaces;

public interface ICirculationService
{
    Task<ServiceResponse<ErrorOr<Unit>>> CreateAsync(CirculationRequest request);
    Task<ServiceResponse<ErrorOr<Unit>>> Update(CirculationRequest request);
    Task<ServiceResponse<ErrorOr<Unit>>> Delete(Guid id);
    Task<ServiceResponse<IReadOnlyCollection<CirculationResponse>>> GetAll(string filterRequest);
}