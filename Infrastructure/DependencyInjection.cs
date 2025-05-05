using System.Net;
using System.Text;
using Application.Abstractions.Interfaces.Repositories;
using Application.Abstractions.Interfaces.Services;
using Ardalis.GuardClauses;
using Domain.Constants;
using Domain.Users;
using Infrastructure.Contexts;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;
using Polly;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddAuthenticationInternal(configuration)
            .AddAuthorizationInternal();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeService, DateTimeService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient(serviceType: typeof(IRepositoryAsync<>), implementationType: typeof(RepositoryAsync<>));

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var productionConnectionString = configuration.GetConnectionString("ProductionDatabase");
        var developmentConnectionString = configuration.GetConnectionString("DevelopmentDatabase");

        Guard.Against.NullOrEmpty(productionConnectionString, "ProductionDatabase connection string not found");
        Guard.Against.NullOrEmpty(developmentConnectionString, "DevelopmentDatabase connection string not found");

        // Registrar interceptores
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Política de reintentos para Oracle 11g
        var retryPolicy = Policy
            .Handle<OracleException>(exceptionPredicate: exception => 
                exception.Number is 3113 or 3114) // ORA-03113, ORA-03114
            .WaitAndRetry(3, sleepDurationProvider: retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        retryPolicy.Execute(() =>
        {
            try
            {
                Console.WriteLine("Connecting to production...");
                ConfigureOracle11GDbContext(services, productionConnectionString);
                Console.WriteLine("Production database connected");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Production connection failed: {ex.Message}");
                Console.WriteLine("Falling back to development...");
                ConfigureOracle11GDbContext(services, developmentConnectionString);
                Console.WriteLine("Development database connected");
            }
        });

        return services;
    }

    private static void ConfigureOracle11GDbContext(
        IServiceCollection services, 
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(optionsAction: (serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            
            options.UseOracle(
                connectionString,
                oracleOptionsAction: oracleOptions =>
                {
                    oracleOptions.MigrationsAssembly(
                        typeof(ApplicationDbContext).Assembly.FullName);
                    
                    // Configuración específica para 11g R2
                    oracleOptions.UseOracleSQLCompatibility("11");
                    oracleOptions.CommandTimeout(180);
                }
            );
            
            options.EnableDetailedErrors()
                   .EnableSensitiveDataLogging();
        });
    }
    
    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddOracle(configuration.GetConnectionString("ProductionDatabase")!);

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configuración de Identity
        services.AddIdentity<User, IdentityRole>(setupAction: options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        });
        
        // Configuración de JWT
        var jwtSettings = configuration.GetSection(key: "JwtSetting");
        services.Configure<JwtSetting>(jwtSettings);

        services.AddAuthentication(configureOptions: options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!)),
                };
                // Manejo detallado de errores
                options.Events = new JwtBearerEvents
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
        
        services.ConfigureApplicationCookie(options => 
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });
        
        services.AddHttpContextAccessor();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
            
        services
            .AddHostedService<RoleSeederService>()
            .AddHostedService<UserSeederService>();
        
        services.AddCors(options => 
        {
            options.AddPolicy(name: "AllowFrontend", configurePolicy: builder => 
                builder.WithOrigins("https://localhost:7159")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
        
        return services;
    }

    private static void AddAuthorizationInternal(this IServiceCollection services)
    {
        // Autorización
        services.AddAuthorization(options =>
        {
            // Acceso completo
            options.AddPolicy(name: PolicyNames.AdminPolicy, configurePolicy: policy => 
                policy.RequireRole(RoleNames.Admin));
    
            // Gestión de circulaciones (Admin + Operator)
            options.AddPolicy(name: PolicyNames.CirculationManagement, configurePolicy: policy => 
                policy.RequireRole(RoleNames.Admin, RoleNames.Operator));
                
            options.AddPolicy(name: PolicyNames.OperatorOnlyPolicy, configurePolicy: policy => 
                policy.RequireRole(RoleNames.Operator));
    
            // Política por defecto (solo autenticación)
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
    }
}