using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Seeds;

public static class DatabaseInitializer
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<User>>();

            // Llamar a los métodos de inicialización
            await DefaultRole.SeedAsync(roleManager);
            await DefaultAdminUser.SeedAsync(userManager, roleManager);
        }
        catch (Exception ex)
        {
            // Manejo de errores, puedes registrar el error aquí
            Console.WriteLine($"Se produjo un error durante la inicialización: {ex.Message}");
        }
    }
}