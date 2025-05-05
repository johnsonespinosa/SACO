using Domain.Constants;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace Infrastructure.Services;

public sealed class UserSeederService(
    IServiceProvider serviceProvider,
    ILogger<UserSeederService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(attempt * 2));

        await retryPolicy.ExecuteAsync(async () => 
        {
            try
            {
                await SeedAdminUserAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error durante la creación del usuario administrador");
                throw;
            }
        });
    }

    private async Task SeedAdminUserAsync(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        // 1. Crear rol Admin si no existe
        if (!await roleManager.RoleExistsAsync(RoleNames.Admin))
        {
            await roleManager.CreateAsync(new IdentityRole(RoleNames.Admin));
        }

        // 2. Obtener credenciales desde appsettings.json
        const string adminUserName = "admin";
        const string adminEmail = "admin@gmail.com";
        const string adminPassword = "admin@123";

        // 3. Verificar si el usuario ya existe
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser is null)
        {
            adminUser = new User
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true // Saltar verificación de email
            };

            // 4. Crear usuario
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            
            if (!result.Succeeded)
            {
                logger.LogError("Error creando usuario administrador: {Errors}", result.Errors);
                throw new Exception($"Error creando usuario: {string.Join(", ", result.Errors)}");
            }
            
            logger.LogInformation("Usuario administrador creado: {Email}", adminEmail);
        }

        // 5. Asignar rol si no lo tiene
        if (!await userManager.IsInRoleAsync(adminUser, RoleNames.Admin))
        {
            await userManager.AddToRoleAsync(adminUser, RoleNames.Admin);
            logger.LogInformation("Rol Admin asignado a: {Email}", adminEmail);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}