using Application;
using HealthChecks.UI.Client;
using Hellang.Middleware.ProblemDetails;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using WebApi.Extensions;
using WebApi.Filters;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(configureLogger: (context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddMvcOptions(options => 
    {
        options.Filters.Add<ValidateModelStateFilter>();
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddProblemDetails(options => 
{
    options.IncludeExceptionDetails = (_, _) => builder.Environment.IsDevelopment();
});

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "SACO API", Version = "v1" });
    options.CustomSchemaIds(type => type.FullName!.Replace('+', '-'));
    options.EnableAnnotations();
    options.OperationFilter<ProblemDetailsOperationFilter>();
    
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Autenticación JWT",
        Description = "Ingrese su token JWT en este campo",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT"
    };

    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            []
        }
    };
    options.AddSecurityRequirement(securityRequirement);
    }
);

var app = builder.Build();

app.UseMiddleware<RequestContextLoggingMiddleware>();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    AllowCachingResponses = false,
    Predicate = _ => true,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
});

await app.RunAsync();