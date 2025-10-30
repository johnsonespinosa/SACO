using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SACO.Application.Models;
using SACO.Domain.Entities;

namespace SACO.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(CreateUserRequest request)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserType = request.UserType
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(new AuthResponse
            {
                Success = false,
                Errors = result.Errors.Select(identityError => identityError.Description).ToArray()
            });
        
        // Assign role based on UserType
        var roleName = request.UserType.ToString();
        await userManager.AddToRoleAsync(user, roleName);

        await signInManager.SignInAsync(user, isPersistent: false);
        
        return new AuthResponse { Success = true, UserId = user.Id, UserName = user.UserName };
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var result = await signInManager.PasswordSignInAsync(
            request.UserName, request.Password, request.RememberMe, lockoutOnFailure: false);

        if (!result.Succeeded)
            return Unauthorized(new AuthResponse { Success = false, Errors = ["Invalid login attempt"] });
        
        var user = await userManager.FindByNameAsync(request.UserName);
        
        return new AuthResponse 
        { 
            Success = true, 
            UserId = user!.Id, 
            UserName = user.UserName,
            UserType = user.UserType
        };

    }

    [HttpPost("logout")]
    public async Task<ActionResult<AuthResponse>> Logout()
    {
        await signInManager.SignOutAsync();
        return new AuthResponse { Success = true };
    }

    [HttpGet("current-user")]
    public async Task<ActionResult<CurrentUserResponse>> GetCurrentUser()
    {
        if (User.Identity?.IsAuthenticated != true)
            return new CurrentUserResponse { IsAuthenticated = false };
        
        var user = await userManager.GetUserAsync(User);
        
        if (user == null)
            return new CurrentUserResponse { IsAuthenticated = false };
        
        var roles = await userManager.GetRolesAsync(user);
        
        return new CurrentUserResponse
        {
            IsAuthenticated = true,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserType = user.UserType,
            Roles = roles
        };

    }
}