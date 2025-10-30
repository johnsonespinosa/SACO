using Ardalis.Specification;
using SACO.Domain.Entities;

namespace SACO.Application.Common.Specifications;

public class PassengerSearchSpecification : Specification<Passenger>
{
    public PassengerSearchSpecification(
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDate = null)
    {
        // Search by at least one field (as required by the document)
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            Query.Where(passenger => passenger.FirstName.Contains(firstName, StringComparison.CurrentCultureIgnoreCase));
        }
        
        if (!string.IsNullOrWhiteSpace(lastName))
        {
            Query.Where(passenger => passenger.FirstLastName.Contains(lastName, StringComparison.CurrentCultureIgnoreCase));
        }
        
        if (birthDate.HasValue)
        {
            Query.Where(passenger => passenger.BirthDate.Date == birthDate.Value.Date);
        }
        
        // Include circulation charts to display related data
        Query.Include(passenger => passenger.Circulations);
        
        // Sort by last name and first name
        Query.OrderBy(passenger => passenger.FirstLastName).ThenBy(passenger => passenger.FirstName);
    }
}