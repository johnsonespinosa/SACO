using Application.DTOs.Circulations;

namespace Application.Interfaces;

public interface ICirculationService
{
    Task<IReadOnlyCollection<CirculationResponse>> GetAll();
}