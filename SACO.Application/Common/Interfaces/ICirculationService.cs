using SACO.Domain.Enums;
using SACO.Shared.Models;

namespace SACO.Application.Common.Interfaces;

public interface ICirculationService
{
    Task<CirculationResponse> CreateOperativeCirculationAsync(CreateCirculationRequest circulation, Guid userId);
    Task<CirculationResponse> ValidateCirculationAsync(Guid circulationId, Guid validatorUserId);
    Task<CirculationResponse> RejectCirculationAsync(Guid circulationId, Guid validatorUserId, string reason);
    Task<IEnumerable<CirculationResponse>> SearchCirculationsAsync(string? firstName, string? lastName, DateTime? birthDate, CirculationStatus? status);
    Task<CirculationResponse?> GetCirculationByIdAsync(Guid circulationId);
    Task CheckExpiredCirculationsAsync();
}