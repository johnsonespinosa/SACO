using Microsoft.AspNetCore.Mvc;
using SACO.Shared.Models;
using SACO.Shared.Services;

namespace SACO.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(CreateUserRequest request)
    {
        var result = await authService.RegisterAsync(request);

        if (!result.Success)
            return BadRequest(result);
        
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var result = await authService.LoginAsync(request);

        if (!result.Success)
            return Unauthorized(result);
        
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult<AuthResponse>> Logout()
    {
        await authService.LogoutAsync();
        return Ok(new AuthResponse { Success = true });
    }

    [HttpGet("current-user")]
    public async Task<ActionResult<CurrentUserResponse>> GetCurrentUser()
    {
        var result = await authService.GetCurrentUserAsync();
        return Ok(result);
    }
}