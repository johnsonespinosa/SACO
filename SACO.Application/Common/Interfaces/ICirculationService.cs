using SACO.Application.Models;
using SACO.Domain.Enums;

namespace SACO.Application.Common.Interfaces;

public interface ICirculationService
{
    Task<CirculationDto> CreateOperativeCirculationAsync(CreateCirculationDto circulation, Guid userId);
    Task<CirculationDto> ValidateCirculationAsync(Guid circulationId, Guid validatorUserId);
    Task<CirculationDto> RejectCirculationAsync(Guid circulationId, Guid validatorUserId, string reason);
    Task<IEnumerable<CirculationDto>> SearchCirculationsAsync(string? firstName, string? lastName, DateTime? birthDate, CirculationStatus? status);
    Task<CirculationDto?> GetCirculationByIdAsync(Guid circulationId);
    Task CheckExpiredCirculationsAsync();
}