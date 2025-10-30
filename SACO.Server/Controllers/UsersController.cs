using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SACO.Application.Common.Interfaces;
using SACO.Application.Models;
using SACO.Domain.Entities;

namespace SACO.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController(IIdentityService identityService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await identityService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        var user = await identityService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] Application.Models.CreateUserRequest request)
    {
        var (result, userId) = await identityService.CreateUserAsync(
            request.UserName,
            request.Password,
            request.Email,
            request.FirstName,
            request.LastName,
            request.UserType);

        if (result.Succeeded)
        {
            return CreatedAtAction(nameof(GetUser), new { id = userId }, new { UserId = userId });
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var result = await identityService.DeleteUserAsync(id);
        if (result.Succeeded) return NoContent();
        return BadRequest(result.Errors);
    }
}

