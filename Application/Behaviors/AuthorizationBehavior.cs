using System.Reflection;
using Application.Exceptions;
using Application.Interfaces;
using Application.Security;

namespace Application.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(IUser user, IIdentityService identityService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUser _user = user ?? throw new ArgumentNullException(nameof(user));
    private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

        if (!authorizeAttributes.Any())
        {
            // No se requiere autorización
            return await next();
        }

        // Debe ser un usuario autenticado
        if (_user.Id == null)
            throw new UnauthorizedAccessException(message: "El usuario debe estar autenticado.");

        // Autorización basada en roles
        await AuthorizeByRoles(authorizeAttributes);
        
        // Autorización basada en políticas
        await AuthorizeByPolicies(authorizeAttributes);

        // El usuario está autorizado
        return await next();
    }

    private async Task AuthorizeByRoles(IEnumerable<AuthorizeAttribute> attributes)
    {
        var rolesToCheck = attributes
            .Where(attribute => !string.IsNullOrWhiteSpace(attribute.Roles))
            .SelectMany(attribute => attribute.Roles.Split(separator: ',', StringSplitOptions.RemoveEmptyEntries))
            .Select(role => role.Trim())
            .Distinct()
            .ToList();

        if (!rolesToCheck.Any()) return;

        var authorized = false;

        foreach (var role in rolesToCheck)
        {
            if (await _identityService.IsInRoleAsync(_user.Id!, role)) 
            {
                authorized = true;
                break;
            }
        }

        if (!authorized)
            throw new ForbiddenAccessException(message: "El usuario no tiene el rol requerido.");
    }

    private async Task AuthorizeByPolicies(IEnumerable<AuthorizeAttribute> attributes)
    {
        var policiesToCheck = attributes
            .Where(attribute => !string.IsNullOrWhiteSpace(attribute.Policy))
            .Select(attribute => attribute.Policy)
            .Distinct()
            .ToList();

        foreach (var policy in policiesToCheck)
        {
            var authorized = await _identityService.AuthorizeAsync(_user.Id!, policy);
            if (!authorized)
                throw new ForbiddenAccessException(message: $"El usuario no está autorizado para la política: {policy}");
        }
    }
}
