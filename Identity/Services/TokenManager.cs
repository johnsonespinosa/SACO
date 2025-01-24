using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Domain.Settings;
using Identity.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services;

public class TokenManager(UserManager<User> userManager, IOptions<JwtSetting> jwtSettings) : ITokenManager
{
    public async Task<JwtSecurityToken> GenerateJwtSecurityToken(User user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach(var role in roles)
            roleClaims.Add(item: new Claim(type: "roles", value: role));

        var ipAddress = IpHelper.GetIpAddress();
        var claims = new[]
        {
            new Claim(type: JwtRegisteredClaimNames.Sub, value: user.UserName!),
            new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new Claim(type: JwtRegisteredClaimNames.Email, value: user.Email!),
            new Claim(type: "uid", value: user.Id),
            new Claim(type: "ip", value: ipAddress),
        }.Union(userClaims).Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256);
        var securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Value.Issuer,
            audience: jwtSettings.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.Value.DurationInMinutes),
            signingCredentials: signingCredentials);

        return securityToken;
    }

    public RefreshSecurityToken GenerateRefreshJwtSecurityToken(string ipAddress)
    {
        return new RefreshSecurityToken
        {
            JwtSecurityToken = Convert.ToBase64String(inArray: RandomNumberGenerator.GetBytes(count: 64)),
            Expire = DateTime.Now.AddDays(7),
            Created = DateTime.Now,
            CreatedByIp = ipAddress,
        };
    }
}