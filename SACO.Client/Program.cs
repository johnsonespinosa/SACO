using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Configure HttpClient to point to the host server
builder.Services.AddScoped(_ => 
    new HttpClient 
    { 
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
    });

builder.Services.AddMudServices();

await builder.Build().RunAsync();
