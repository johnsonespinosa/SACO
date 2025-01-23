using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications;

public sealed class GetAllOperationalCirculationsSpecification : Specification<OperationalCirculation>
{
    public GetAllOperationalCirculationsSpecification(string filterRequest)
    {
        // Verificamos que no esté eliminada
        Query.Where(operationalCirculation => operationalCirculation.Deleted == null &&
                                              string.IsNullOrEmpty(operationalCirculation.DeletedBy));
        
        // Si hay un filtro, aplicamos los criterios de búsqueda
        if (!string.IsNullOrWhiteSpace(filterRequest))
        {
            var filterCriteria = filterRequest.ToLower();
                
            Query.Where(operationalCirculation => 
                operationalCirculation.FirstName.ToLower().Contains(filterCriteria) ||
                operationalCirculation.LastName1.ToLower().Contains(filterCriteria) ||
                operationalCirculation.LastName2.ToLower().Contains(filterCriteria) ||
                operationalCirculation.BirthDate.Date == DateTime.Parse(filterCriteria));
        }
    }
}