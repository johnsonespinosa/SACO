using Ardalis.Specification;
using SACO.Domain.Entities;

namespace SACO.Application.Common.Specifications;

public class PassengerWithCirculationsSpecification : Specification<Passenger>
{
    public PassengerWithCirculationsSpecification(Guid passengerId)
    {
        Query.Where(passenger => passenger.Id == passengerId)
            .Include(passenger => passenger.Circulations)
            .ThenInclude(circulation => circulation.Organ)
            .Include(passenger => passenger.Circulations)
            .ThenInclude(circulation => circulation.Department);
    }
}