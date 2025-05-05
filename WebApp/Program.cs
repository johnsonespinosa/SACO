using Application.Abstractions.Interfaces.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using WebApp;
using WebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>(selector: "#app");
builder.RootComponents.Add<HeadOutlet>(selector: "head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7159/") });

// Servicio de autenticación personalizado
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<ISessionStorageService, SessionStorageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICirculationService, CirculationService>();
builder.Services.AddScoped<ICirculationTypeService, CirculationTypeService>();
builder.Services.AddScoped<ICitizenshipService, CitizenshipService>();
builder.Services.AddScoped<IExpirationService, ExpirationService>();
builder.Services.AddScoped<IOrganService, OrganService>();

// Configuración de bibliotecas externas
builder.Services.AddMudServices(configuration =>
{
    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
    configuration.SnackbarConfiguration.PreventDuplicates = true;
    configuration.SnackbarConfiguration.NewestOnTop = true;
    configuration.SnackbarConfiguration.ShowCloseIcon = true;
    configuration.SnackbarConfiguration.VisibleStateDuration = 5000;
});
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();