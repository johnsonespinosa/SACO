using System.Net;
using System.Text;
using Application.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Settings;
using EntityFramework.Exceptions.PostgreSQL;
using Identity.Contexts;
using Identity.Repositories;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Interfaces;

namespace Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(name: "DefaultConnection");

        Guard.Against.Null(input: connectionString, message: "No se encontró la cadena de conexión 'DefaultConnection'.");

        services.AddDbContext<ApplicationIdentityDbContext>(optionsAction: options =>
        {
            // Configure the database provider
            options.UseNpgsql(connectionString, npgsqlOptionsAction: npgsqlOptions =>
            {
                // Defines the assembly where the migrations will be generated
                npgsqlOptions.MigrationsAssembly(typeof(ApplicationIdentityDbContext).Assembly.FullName);
        
                // Enable retries on failure
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,                          //Maximum number of retries
                    maxRetryDelay: TimeSpan.FromSeconds(10),   // Time between retries
                    errorCodesToAdd: null                       // Additional error codes to consider for retries
                );
            }).UseExceptionProcessor(); // Enables exception processing
        });
        
        services.AddDefaultIdentity<User>(configureOptions: options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                    options.Password.RequireDigit = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredUniqueChars = 1;
                }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(configureOptions: options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSetting:Issuer"],
                    ValidAudience = configuration["JwtSetting:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:Key"]!))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Status = StatusCodes.Status401Unauthorized,
                            Title = "Error de autenticación",
                            Detail = "Token no válido o error de autenticación.",
                            Instance = context.Request.Path
                        };

                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/problem+json";
                        return context.Response.WriteAsJsonAsync(problemDetails);
                    },
                    OnChallenge = context =>
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Status = StatusCodes.Status401Unauthorized,
                            Title = "Autorización fallida.",
                            Detail = "No está autorizado para acceder a este recurso.",
                            Instance = context.Request.Path
                        };

                        context.HandleResponse();
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/problem+json";
                        return context.Response.WriteAsJsonAsync(problemDetails);
                    },
                    OnForbidden = context =>
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Status = StatusCodes.Status403Forbidden,
                            Title = "Acceso denegado",
                            Detail = "No tienes permiso para acceder a este recurso.",
                            Instance = context.Request.Path
                        };

                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/problem+json";
                        return context.Response.WriteAsJsonAsync(problemDetails);
                    }
                };
            });

            services.AddAuthorizationBuilder();
 
            services.Configure<JwtSetting>(configuration.GetSection(key: "JwtSetting"));

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}