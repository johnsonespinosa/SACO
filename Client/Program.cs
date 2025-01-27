using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Middlewares;
using Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using MudBlazor.Services;
using Shared.Interfaces;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configura el componente raíz
builder.RootComponents.Add<App>(selector: "#app");
builder.RootComponents.Add<HeadOutlet>(selector: "head::after");

// Registra el interceptor de HttpClient
builder.Services.AddHttpClientInterceptor();

// Configura HttpClient con la dirección base y habilita el interceptor
builder.Services.AddScoped<ErrorHandler>();
builder.Services.AddHttpClient(name: "API", configureClient: client =>
{
    client.BaseAddress = new Uri("https://localhost:7163");
}).AddHttpMessageHandler<ErrorHandler>();

// Registro de servicios
builder.Services.AddScoped<ICirculationTypeService, CirculationTypeService>();
builder.Services.AddScoped<ICitizenshipService, CitizenshipService>();
builder.Services.AddScoped<IExpirationService, ExpirationService>();
builder.Services.AddScoped<ICirculationService, CirculationService>();
builder.Services.AddScoped<IOrganService, OrganService>();
builder.Services.AddScoped<IUserService, UserService>();

// Configuración de bibliotecas externas
builder.Services.AddMudServices();
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();