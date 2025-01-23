using Application.Interfaces;
using Ardalis.GuardClauses;
using EntityFramework.Exceptions.PostgreSQL;
using Infrastructure.Contexts;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(name: "DefaultConnection");

        Guard.Against.Null(input: connectionString, message: "No se encontró la cadena de conexión 'DefaultConnection'.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>(optionsAction: (serviceProvider, options) =>
        {
            // Add interceptors to handle context changes
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());

            // Configure the database provider
            options.UseNpgsql(connectionString, npgsqlOptionsAction: npgsqlOptions =>
            {
                // Defines the assembly where the migrations will be generated
                npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        
                // Enable retries on failure
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,                          //Maximum number of retries
                    maxRetryDelay: TimeSpan.FromSeconds(10),   // Time between retries
                    errorCodesToAdd: null                       // Additional error codes to consider for retries
                );
            }).UseExceptionProcessor(); // Enables exception processing
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<ITimeProvider, SystemTimeProvider>();

        services.AddTransient(serviceType: typeof(IRepositoryAsync<>), implementationType: typeof(RepositoryAsync<>));
        
        return services;
    }
}