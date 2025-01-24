using Api.Controllers.Bases;
using Application.DTOs.Users;
using Application.Models;
using Application.UseCases.Users.Commands.Authenticate;
using Application.UseCases.Users.Commands.Create;
using Application.UseCases.Users.Commands.Delete;
using Application.UseCases.Users.Commands.Update;
using Application.UseCases.Users.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class UsersController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var readOnlyCollection = await sender.Send(request: new GetAllUsersQuery());
        return Ok(readOnlyCollection);
    }
    
    [HttpPost(template: "Authenticate")]
    public async Task<ActionResult<ServiceResponse<AuthenticationResponse>>> Authenticate([FromBody] AuthenticationRequest request)
    {
        var command = new AuthenticateUserCommand(request, IpAddress: GenerateIpAddress());
        var response = await sender.Send(command);
        return Ok(response);
    }

    // [Authorize(Roles = "Administrator")]
    [HttpPost(template: "Create")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await sender.Send(request: new CreateUserCommand(request));
        return result.Match(
            onValue: unit => Ok(unit),
            onError: Problem);
    }

    [HttpPut(template: "Update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var result = await sender.Send(request: new UpdateUserCommand(request));
        return result.Match(
            onValue: unit => Ok(unit),
            onError: Problem);
    }

    [HttpDelete(template: "Delete")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await sender.Send(request: new DeleteUserCommand(id));
        return result.Match(
            onValue: unit => Ok(unit),
            onError: Problem);
    }

    private string GenerateIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-for"))
            return Request.Headers["X-Forwarded-for"]!;
            
        return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
    }
}