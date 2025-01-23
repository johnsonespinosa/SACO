using System.Reflection;
using Application.Behaviors;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddBehavior(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(UnhandledExceptionBehavior<,>));
            configuration.AddBehavior(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(AuthorizationBehavior<,>));
            configuration.AddBehavior(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(ValidationBehavior<,>));
            configuration.AddBehavior(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(PerformanceBehavior<,>));
        });

        services.AddScoped<IOperationalCirculationService, OperationalCirculationService>();
        services.AddScoped<IOrganService, OrganService>();
        services.AddScoped<ICitizenshipService, CitizenshipService>();
        services.AddScoped<ICirculationService, CirculationService>();
        services.AddScoped<IExpirationService, ExpirationService>();

        return services;
    }
}