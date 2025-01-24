using Application.DTOs.Users;

namespace Application.Interfaces;

public interface IUserService
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
    Task<ErrorOr<Unit>> AddAsync(CreateUserRequest request);
    Task<ErrorOr<Unit>> UpdateAsync(UpdateUserRequest request);
    Task<ErrorOr<Unit>> DeleteAsync(string userId);
    Task<IReadOnlyCollection<UserResponse>> GetAllAsync();
}