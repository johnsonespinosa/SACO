using Application.DTOs.OperationalCirculations;

namespace Application.Interfaces;

public interface IOperationalCirculationService
{
    Task<ErrorOr<Unit>> Add(CreateOperationalCirculationRequest request);
    Task<ErrorOr<Unit>> Update(UpdateOperationalCirculationRequest request);
    Task<ErrorOr<Unit>> Delete(Guid id);
    Task<IReadOnlyCollection<OperationalCirculationResponse>> GetAll(string filterRequest);
}