using Ardalis.Specification;
using SACO.Domain.Entities;
using SACO.Domain.Enums;

namespace SACO.Application.Common.Specifications;

public class CirculationWithDetailsSpecification : Specification<Circulation>
{
    public CirculationWithDetailsSpecification(
        CirculationStatus? status = null,
        CirculationType? type = null)
    {
        Query.Include(circulation => circulation.Passenger)
            .Include(circulation => circulation.Organ)
            .Include(circulation => circulation.Department)
            .Include(circulation => circulation.CreatorUser)
            .Include(circulation => circulation.ValidatorUser);
             
        if (status.HasValue)
        {
            Query.Where(circulation => circulation.Status == status.Value);
        }
        
        if (type.HasValue)
        {
            Query.Where(circulation => circulation.Type == type.Value);
        }
        
        Query.OrderByDescending(circulation => circulation.CreatedAt);
    }
}