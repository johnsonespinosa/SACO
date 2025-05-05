using Application.Abstractions.DTOs;
using Domain.Models;

namespace Application.Abstractions.Interfaces.Services;

public interface IExpirationService
{
    Task<Result<IReadOnlyCollection<ExpirationDto>>> GetAllAsync();
}