using Microsoft.AspNetCore.Identity;
using SACO.Application.Common.Interfaces;
using SACO.Application.Models;
using SACO.Domain.Entities;
using SACO.Domain.Enums;

namespace SACO.Application.Services;

public class IdentityService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    : IIdentityService
{
    public async Task<string?> GetUserNameAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return user?.UserName;
    }

    public async Task<bool> IsInRoleAsync(Guid userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return user != null && await userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
    {
        // Implementar lógica de políticas si es necesario
        return await IsInRoleAsync(userId, policyName);
    }

    public async Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password, string email, string firstName, string lastName, UserType userType)
    {
        var user = new User
        {
            UserName = userName,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            UserType = userType
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            // Asignar rol basado en UserType
            var roleName = userType.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
            
            await userManager.AddToRoleAsync(user, roleName);
        }

        return (result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description)), user.Id);
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return user == null ? Result.Failure(new[] { "User not found" }) : 
            await DeleteUserAsync(user);
    }

    public async Task<Result> DeleteUserAsync(User user)
    {
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return userManager.Users.ToList();
    }

    public async Task<List<User>> GetUsersInRoleAsync(string roleName)
    {
        return (await userManager.GetUsersInRoleAsync(roleName)).ToList();
    }
}