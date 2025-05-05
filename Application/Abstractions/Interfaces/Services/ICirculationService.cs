using Application.Abstractions.DTOs;
using Domain.Models;

namespace Application.Abstractions.Interfaces.Services;

public interface ICirculationService
{
    Task<Result> CreateAsync(CirculationDto dto);
    Task<Result> UpdateAsync(CirculationDto dto);
    Task<Result> DeleteAsync(Guid circulationId);
    Task<Result<PaginatedResult<CirculationDto>>> GetAllAsync(PaginationDto paginationDto);
    Task<Result<CirculationDto>> GetById(Guid circulationId);
}