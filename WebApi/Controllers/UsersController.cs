using Application.Features.Commands.Users.CreateUser;
using Application.Features.Commands.Users.DeleteUser;
using Application.Features.Commands.Users.UpdateUser;
using Application.Features.Queries.Users.GetUserById;
using Application.Features.Queries.Users.GetUsers;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Bases;

namespace WebApi.Controllers;

[Route(template: "api/users")]
public class UsersController(ISender sender) : CustomController
{
    [HttpPost]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await sender.Send(command);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetUserById), routeValues: new { id = result.Value }, result.Value)
            : BadRequest(result.Errors);
    }

    [HttpGet(template: "{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(string id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await sender.Send(query);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Errors);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        var result = await sender.Send(query);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
    {
        // Verificar coincidencia de IDs
        if (id != command.Id)
            return BadRequest(error: "ID de usuario no coincide");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Errors);
    }

    [HttpDelete(template: "{id}")]
    [Authorize(Roles = RoleNames.Admin)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var command = new DeleteUserCommand(id);
        var result = await sender.Send(command);

        return result.IsSuccess
            ? NoContent()
            : BadRequest(result.Errors);
    }
}