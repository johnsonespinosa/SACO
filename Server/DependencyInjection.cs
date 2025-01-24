using Application.Interfaces;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddServerServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy(name: "CorsPolicy", configurePolicy: builder =>
            {
                builder.WithOrigins("https://localhost:7163")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }
}