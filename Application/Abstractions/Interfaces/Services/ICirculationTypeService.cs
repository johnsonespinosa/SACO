using Application.Abstractions.DTOs;
using Domain.Models;

namespace Application.Abstractions.Interfaces.Services;

public interface ICirculationTypeService
{
    Task<Result<IReadOnlyCollection<CirculationTypeDto>>> GetAllAsync();
}