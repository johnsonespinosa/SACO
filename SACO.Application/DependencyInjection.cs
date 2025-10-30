using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using SACO.Application.Common.Interfaces;
using SACO.Application.Services;

namespace SACO.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register MediatR
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Register AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        // Register FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Register services
        services.AddScoped<ICirculationService, CirculationService>();
        services.AddScoped<IPassengerService, PassengerService>();
        
        return services;
    }
}