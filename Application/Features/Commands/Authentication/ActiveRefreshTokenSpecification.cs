using Ardalis.Specification;
using Domain.Authentication;

namespace Application.Features.Commands.Authentication;

public sealed class ActiveRefreshTokenSpecification : Specification<RefreshToken>
{
    public ActiveRefreshTokenSpecification(string token, string userId)
    {
        Query.Where(refreshToken => 
            refreshToken.Token == token &&
            refreshToken.UserId == userId &&
            !refreshToken.IsRevoked &&
            !refreshToken.IsExpired
        );
    }
}