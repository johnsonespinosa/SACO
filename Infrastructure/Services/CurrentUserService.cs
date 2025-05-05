using System.Security.Claims;
using Application.Abstractions.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

internal sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string UserId
    {
        get
        {
            var httpContext = httpContextAccessor.HttpContext 
                              ?? throw new InvalidOperationException("HTTP context no disponible.");
            
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) 
                         ?? throw new InvalidOperationException("Usuario no autenticado.");
            
            return userId;
        }
    }
}