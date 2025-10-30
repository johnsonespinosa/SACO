using AutoMapper;
using SACO.Application.Common.Interfaces;
using SACO.Application.Common.Specifications;
using SACO.Application.Models;
using SACO.Domain.Entities;

namespace SACO.Application.Services;

public class PassengerService(IUnitOfWork unitOfWork, IMapper mapper) : IPassengerService
{
    public async Task<PassengerDto> CreatePassengerAsync(CreatePassengerDto createDto)
    {
        var passenger = mapper.Map<Passenger>(createDto);
        passenger.CalculateSearchKey();
        
        await unitOfWork.Passengers.AddAsync(passenger);
        await unitOfWork.SaveChangesAsync();
        
        return mapper.Map<PassengerDto>(passenger);
    }

    public async Task<IEnumerable<PassengerDto>> SearchPassengersAsync(string? firstName, string? lastName, DateTime? birthDate)
    {
        var spec = new PassengerSearchSpecification(firstName, lastName, birthDate);
        var passengers = await unitOfWork.Passengers.ListAsync(spec);
        
        return mapper.Map<IEnumerable<PassengerDto>>(passengers);
    }

    public async Task<PassengerDto?> GetPassengerWithCirculationsAsync(Guid passengerId)
    {
        var spec = new PassengerWithCirculationsSpecification(passengerId);
        var passenger = await unitOfWork.Passengers.FirstOrDefaultAsync(spec);
        
        return passenger != null ? mapper.Map<PassengerDto>(passenger) : null;
    }
}