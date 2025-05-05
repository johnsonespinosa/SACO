using System.Reflection;
using Application.Abstractions.Behaviors;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        // Configuración de AutoMapper
        services.AddSingleton<IMapper>(_ => 
        {
            var configuration = new MapperConfiguration(configure: configurationExpression =>
            {
                configurationExpression.AddMaps(assembliesToScan: Assembly.GetExecutingAssembly());
                configurationExpression.ShouldMapProperty = propertyInfo => propertyInfo.GetMethod?.IsPublic == true || propertyInfo.GetMethod?.IsPrivate == true;
            });
            
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        });
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
    }
}