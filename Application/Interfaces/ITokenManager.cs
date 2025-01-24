using System.IdentityModel.Tokens.Jwt;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITokenManager
{
    Task<JwtSecurityToken> GenerateJwtSecurityToken(User user);
    RefreshSecurityToken GenerateRefreshJwtSecurityToken(string ipAddress);
}