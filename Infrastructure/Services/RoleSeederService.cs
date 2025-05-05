using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace Infrastructure.Services;

public sealed class RoleSeederService(
    IServiceProvider serviceProvider,
    ILogger<RoleSeederService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(attempt * 2));

        await retryPolicy.ExecuteAsync(async () => 
        {
            try
            {
                await SeedRolesAsync(roleManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error durante la creación de roles");
                throw;
            }
        });
    }

    private async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in new[] { RoleNames.Admin, RoleNames.Operator })
        {
            var normalizedRole = role.ToUpperInvariant();
            if (await roleManager.RoleExistsAsync(normalizedRole)) continue;
            
            var result = await roleManager.CreateAsync(new IdentityRole(role));
            if (!result.Succeeded)
            {
                logger.LogError("Error creando rol {Role}: {Errors}", role, result.Errors);
                throw new Exception($"Error creando rol {role}");
            }
            logger.LogInformation("Rol creado: {Role}", role);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}