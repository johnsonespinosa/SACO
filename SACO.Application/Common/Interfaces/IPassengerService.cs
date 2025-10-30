using SACO.Application.Models;

namespace SACO.Application.Common.Interfaces;

public interface IPassengerService
{
    Task<PassengerDto> CreatePassengerAsync(CreatePassengerDto createDto);
    Task<IEnumerable<PassengerDto>> SearchPassengersAsync(string? firstName, string? lastName, DateTime? birthDate);
    Task<PassengerDto?> GetPassengerWithCirculationsAsync(Guid passengerId);
}