using System.Reflection;
using Application.Behaviors;
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

        // services.AddScoped<IOperationalCirculationService, OperationalCirculationService>();
        // services.AddScoped<IOrganService, OrganService>();
        // services.AddScoped<ICountryCodeService, CountryCodeService>();
        // services.AddScoped<ICirculationCodeService, CirculationCodeService>();
        // services.AddScoped<IExpirationCodeService, ExpirationCodeService>();

        return services;
    }
}