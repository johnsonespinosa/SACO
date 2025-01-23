using Application.DTOs.Users;
using Application.Models;

namespace Application.Interfaces;

public interface IIdentityService
{
    Task<ServiceResponse<UserResponse>> GetUserById(string id);
    Task<ServiceResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    Task<ServiceResponse<string>> CreateUserAsync(CreateUserRequest request, string origin);
    Task<ServiceResponse<string>> UpdateUserAsync(UpdateUserRequest request);
    Task<ServiceResponse<string>> DeleteUserAsync(string userId);
    Task<string?> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
}