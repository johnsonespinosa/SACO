using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications;

public sealed class GetAllUserSpecification : Specification<User>
{
    public GetAllUserSpecification(string filterRequest)
    {
        // Si hay un filtro, aplicamos los criterios de búsqueda
        if (!string.IsNullOrWhiteSpace(filterRequest))
        {
            var filterCriteria = filterRequest.ToLower();
                
            Query.Where(user => 
                user.UserName!.ToLower().Contains(filterCriteria) ||
                user.Email!.ToLower().Contains(filterCriteria));
        }
    }
}