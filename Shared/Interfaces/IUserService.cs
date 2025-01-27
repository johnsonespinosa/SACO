using ErrorOr;
using MediatR;
using Shared.DTOs;
using Shared.DTOs.Users;

namespace Shared.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    Task<ServiceResponse<ErrorOr<Unit>>> AddAsync(UserRequest request);
    Task<ServiceResponse<ErrorOr<Unit>>> UpdateAsync(UserRequest request);
    Task<ServiceResponse<ErrorOr<Unit>>> DeleteAsync(string userId);
    Task<ServiceResponse<IReadOnlyCollection<UserResponse>>> GetAllAsync(string filterRequest);
    Task<ServiceResponse<UserResponse>> GetById(string id);
}