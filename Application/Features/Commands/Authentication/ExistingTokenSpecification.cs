using Ardalis.Specification;
using Domain.Authentication;

namespace Application.Features.Commands.Authentication;

public sealed class ExistingTokenSpecification : Specification<RefreshToken>
{
    public ExistingTokenSpecification(string userId)
    {
        Query.Where(refreshToken => 
            refreshToken.UserId == userId && 
            !refreshToken.Revoked.HasValue &&
            refreshToken.Expire > DateTimeOffset.UtcNow);
    }
}