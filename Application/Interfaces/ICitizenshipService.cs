using Application.DTOs.Citizenships;

namespace Application.Interfaces;

public interface ICitizenshipService
{
    Task<IReadOnlyCollection<CitizenshipResponse>> GetAll();
}