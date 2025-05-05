using Application.Abstractions.DTOs;
using Domain.Models;

namespace Application.Abstractions.Interfaces.Services;

public interface IUserService
{
    Task<Result> CreateAsync(UserCreateDto dto);
    Task<Result> UpdateAsync(UserUpdateDto dto);
    Task<Result> DeleteAsync(string userId);
    Task<Result<PaginatedResult<UserDto>>> GetAllAsync(PaginationDto paginationDto);
    Task<Result<UserDto>> GetById(string userId);
}