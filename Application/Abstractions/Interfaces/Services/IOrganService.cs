using Application.Abstractions.DTOs;
using Domain.Models;

namespace Application.Abstractions.Interfaces.Services;

public interface IOrganService
{
    Task<Result<IReadOnlyCollection<OrganDto>>> GetAllAsync();
}