using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Authentication;
using Domain.Users;

namespace Application.Abstractions.Interfaces.Services;

public interface ITokenService
{
    Task<JwtSecurityToken> GenerateAccessToken(User user, string ipAddress);
    RefreshToken GenerateRefreshToken(string userId, string ipAddress);
    TimeSpan  GetTokenDuration();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}