using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SACO.Client.Services;
using SACO.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HttpClient to point to the host server
builder.Services.AddScoped(_ => 
    new HttpClient 
    { 
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
    });

// Register authentication services
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
