using MudBlazor.Services;
using SACO.Application;
using SACO.Components;
using SACO.Infrastructure;
using _Imports = SACO.Client._Imports;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add Infrastructure Layer (Database, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application Layer (MediatR, Services)
builder.Services.AddApplication();

// Add HttpClient for API calls
builder.Services.AddHttpClient();

// Add API Controllers
builder.Services.AddControllers(); 

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    // Enable Swagger in Development
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}
else
{
    app.UseExceptionHandler(errorHandlingPath: "/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

// Map API Controllers
app.MapControllers(); 

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(_Imports).Assembly);

app.Run();