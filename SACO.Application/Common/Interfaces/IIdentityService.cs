using SACO.Application.Models;
using SACO.Domain.Entities;
using SACO.Domain.Enums;

namespace SACO.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);
    Task<bool> IsInRoleAsync(Guid userId, string role);
    Task<bool> AuthorizeAsync(Guid userId, string policyName);
    Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password, string email, string firstName, string lastName, UserType userType);
    Task<Result> DeleteUserAsync(Guid userId);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<List<User>> GetUsersAsync();
    Task<List<User>> GetUsersInRoleAsync(string roleName);
}

