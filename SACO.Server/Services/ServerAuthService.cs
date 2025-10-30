using Microsoft.AspNetCore.Identity;
using SACO.Domain.Entities;
using SACO.Shared.Models;
using SACO.Shared.Services;

namespace SACO.Services;

public class ServerAuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IHttpContextAccessor contextAccessor) : IAuthService
{
    public async Task<AuthResponse> LoginAsync(LoginRequest loginRequest)
    {
        var result = await signInManager.PasswordSignInAsync(
            loginRequest.UserName, loginRequest.Password, loginRequest.RememberMe, lockoutOnFailure: false);

        if (!result.Succeeded)
            return new AuthResponse
            {
                Success = false,
                Errors = ["Invalid login attempt"]
            };
        
        var user = await userManager.FindByNameAsync(loginRequest.UserName);
        
        return new AuthResponse 
        { 
            Success = true, 
            UserId = user!.Id, 
            UserName = user.UserName,
            UserType = user.UserType
        };

    }

    public async Task<AuthResponse> RegisterAsync(CreateUserRequest registerRequest)
    {
        var user = new User
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            UserType = registerRequest.UserType
        };

        var result = await userManager.CreateAsync(user, registerRequest.Password);

        if (!result.Succeeded)
            return new AuthResponse
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToArray()
            };
        
        // Asignar rol basado en UserType
        var roleName = registerRequest.UserType.ToString();
        await userManager.AddToRoleAsync(user, roleName);

        await signInManager.SignInAsync(user, isPersistent: false);
        return new AuthResponse { Success = true, UserId = user.Id, UserName = user.UserName };

    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<CurrentUserResponse> GetCurrentUserAsync()
    {
        var user = contextAccessor.HttpContext?.User;
        
        if (user?.Identity?.IsAuthenticated != true)
            return new CurrentUserResponse { IsAuthenticated = false };
        
        var appUser = await userManager.GetUserAsync(user);
        
        if (appUser == null)
            return new CurrentUserResponse { IsAuthenticated = false };
        
        var roles = await userManager.GetRolesAsync(appUser);
        
        return new CurrentUserResponse
        {
            IsAuthenticated = true,
            UserName = appUser.UserName,
            Email = appUser.Email,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            UserType = appUser.UserType,
            Roles = roles
        };

    }
}