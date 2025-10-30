using SACO.Shared.Models;

namespace SACO.Application.Common.Interfaces;

public interface IPassengerService
{
    Task<PassengerResponse> CreatePassengerAsync(CreatePassengerRequest createDto);
    Task<IEnumerable<PassengerResponse>> SearchPassengersAsync(string? firstName, string? lastName, DateTime? birthDate);
    Task<PassengerResponse?> GetPassengerWithCirculationsAsync(Guid passengerId);
}