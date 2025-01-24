using Api;
using Api.Middlewares;
using Application;
using Identity;
using Identity.Seeds;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuración inicial de Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Establece el nivel mínimo de registro
    .WriteTo.Console() // Registra en la consola
    .WriteTo.File(path: "logs/log-.txt", rollingInterval: RollingInterval.Day) // Registra en un archivo con rotación diaria
    .Enrich.FromLogContext() // Enriquecer los logs con el contexto de la solicitud
    .CreateBootstrapLogger(); // Crea un logger inicial para capturar errores durante la configuración

builder.Host.UseSerilog(configureLogger: (context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration) // Lee la configuración desde appsettings.json
    .Enrich.FromLogContext()); // Enriquecer los logs con el contexto de la solicitud

// Configurar servicios de la aplicación
builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddServerServices();

// Registrar el middleware global de excepciones
builder.Services.AddScoped<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // Esto es solo para desarrollo
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseCors();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();

// Usar el middleware de manejo global de excepciones
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Mapear controladores
app.MapControllers();

// Inicializar roles y usuario administrador
await DatabaseInitializer.SeedAsync(app.Services);

app.Run();

public partial class Program { }