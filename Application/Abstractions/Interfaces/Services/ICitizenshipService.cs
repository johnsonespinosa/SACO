using Application.Abstractions.DTOs;
using Domain.Models;

namespace Application.Abstractions.Interfaces.Services;

public interface ICitizenshipService
{
    Task<Result<IReadOnlyCollection<CitizenshipDto>>> GetAllAsync();
}