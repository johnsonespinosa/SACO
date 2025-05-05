using Application.Abstractions.DTOs;
using Domain.Models;

namespace Application.Abstractions.Interfaces.Services;

public interface IAuthService
{
    Task<Result<AuthResponse>> Authenticate(AuthRequest request);
    Task Logout();
    Task<Result<AuthResponse>> RefreshToken();
    Task<Result<UserSessionDto>> GetUserSession(); // Nuevo método
}