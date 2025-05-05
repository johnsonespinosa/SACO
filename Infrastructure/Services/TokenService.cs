using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.Interfaces.Services;
using Domain.Authentication;
using Domain.Users;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

internal sealed class TokenService(UserManager<User> userManager, IOptions<JwtSetting> jwtSettings) : ITokenService
{
    public async Task<JwtSecurityToken> GenerateAccessToken(User user, string ipAddress)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList(); 
        var claims = new[]
        {
            new Claim(type: JwtRegisteredClaimNames.Sub, value: user.UserName!),
            new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new Claim(type: "uid", value: user.Id),
            new Claim(type: "ip", value: ipAddress)
        }.Union(userClaims).Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256);
        var securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Value.Issuer,
            audience: jwtSettings.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.Value.AccessTokenDurationMinutes),
            signingCredentials: signingCredentials);

        return securityToken;
    }

    public RefreshToken GenerateRefreshToken(string userId, string ipAddress)
    {
        // Generar token aleatorio (128 caracteres en base64)
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        var token = Convert.ToBase64String(randomBytes)
            .Replace("+", "-")  // Asegurar URL-safe
            .Replace("/", "_")
            .TrimEnd('=');

        return new RefreshToken
        {
            UserId = userId,
            Token = token,
            Expire = DateTimeOffset.UtcNow.AddDays(jwtSettings.Value.RefreshTokenDurationDays),
            CreatedByIp = ipAddress  // IP del cliente
        };
    }

    public TimeSpan GetTokenDuration() => 
        TimeSpan.FromMinutes(jwtSettings.Value.AccessTokenDurationMinutes);

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key)),
            ValidateLifetime = false // Permitir tokens expirados
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
        
        return principal;
    }
}