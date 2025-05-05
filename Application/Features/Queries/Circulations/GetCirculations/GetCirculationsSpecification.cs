using System.Globalization;
using Ardalis.Specification;
using Domain.Circulations;

namespace Application.Features.Queries.Circulations.GetCirculations;

public sealed class GetCirculationsSpecification : Specification<Circulation>
{
    public GetCirculationsSpecification(string? filter, int pageSize, int pageNumber)
    {
        Query
            .Include(circulation => circulation.Expiration)
            .Include(circulation => circulation.Organ)
            .Include(circulation => circulation.Citizenship)
            .Include(circulation => circulation.CirculationType)
            .OrderByDescending(orderExpression: circulation => circulation.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
        
        if (!string.IsNullOrWhiteSpace(filter))
        {
            ApplyFilter(filter.Trim());
        }
    }
    
    private void ApplyFilter(string filter)
    {
        // Búsqueda por fecha con formato específico
        if (DateTime.TryParseExact(filter, "yyyy.MM.dd", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            Query.Where(circulation => circulation.BirthDate.ToString() == date.ToString("yyyy.MM.dd"));
            return;
        }

        // Búsqueda case-insensitive alternativa
        var lowerFilter = filter.ToLower();
        Query.Where(circulation => 
            circulation.FirstName.ToLower().Contains(lowerFilter) ||
            circulation.LastName1.ToLower().Contains(lowerFilter));
    }
}