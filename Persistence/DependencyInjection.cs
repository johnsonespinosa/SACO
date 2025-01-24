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

        // Registrar interceptores
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Configurar DbContext
        services.AddDbContext<ApplicationDbContext>(optionsAction: (serviceProvider, options) =>
        {
            // Agregar interceptores al contexto
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());

            // Configurar el proveedor de base de datos
            options.UseNpgsql(connectionString, npgsqlOptionsAction: npgsqlOptions =>
            {
                // Define el ensamblado donde se generarán las migraciones
                npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        
                // Habilitar reintentos en caso de fallo
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,                          // Número máximo de reintentos
                    maxRetryDelay: TimeSpan.FromSeconds(10),   // Tiempo entre reintentos
                    errorCodesToAdd: null                      // Códigos de error adicionales a tener en cuenta para los reintentos
                );
            }).UseExceptionProcessor(); // Habilita el procesamiento de excepciones
        });

        // Proveer el contexto a través de la interfaz
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        // Registrar proveedor de tiempo
        services.AddScoped<ITimeProvider, SystemTimeProvider>();

        // Registrar repositorio genérico
        services.AddTransient(serviceType: typeof(IRepositoryAsync<>), implementationType: typeof(RepositoryAsync<>));
        
        return services;
    }
}