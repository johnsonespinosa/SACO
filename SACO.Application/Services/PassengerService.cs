using AutoMapper;
using SACO.Application.Common.Interfaces;
using SACO.Application.Common.Specifications;
using SACO.Domain.Entities;
using SACO.Shared.Models;

namespace SACO.Application.Services;

public class PassengerService(IUnitOfWork unitOfWork, IMapper mapper) : IPassengerService
{
    public async Task<PassengerResponse> CreatePassengerAsync(CreatePassengerRequest createDto)
    {
        var passenger = mapper.Map<Passenger>(createDto);
        passenger.CalculateSearchKey();
        
        await unitOfWork.Passengers.AddAsync(passenger);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<PassengerResponse>(passenger);
    }

    public async Task<IEnumerable<PassengerResponse>> SearchPassengersAsync(string? firstName, string? lastName, DateTime? birthDate)
    {
        var spec = new PassengerSearchSpecification(firstName, lastName, birthDate);
        var passengers = await unitOfWork.Passengers.ListAsync(spec);
        
        return mapper.Map<IEnumerable<PassengerResponse>>(passengers);
    }

    public async Task<PassengerResponse?> GetPassengerWithCirculationsAsync(Guid passengerId)
    {
        var spec = new PassengerWithCirculationsSpecification(passengerId);
        var passenger = await unitOfWork.Passengers.FirstOrDefaultAsync(spec);
        
        return passenger != null ? mapper.Map<PassengerResponse>(passenger) : null;
    }
}