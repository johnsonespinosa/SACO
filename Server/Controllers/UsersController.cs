using Api.Controllers.Bases;
using Application.UseCases.Users.Commands.Authenticate;
using Application.UseCases.Users.Commands.Create;
using Application.UseCases.Users.Commands.Delete;
using Application.UseCases.Users.Commands.Update;
using Application.UseCases.Users.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.DTOs.Users;

namespace Api.Controllers;

/// <summary>
/// Controlador para gestionar las operaciones relacionadas con los usuarios.
/// </summary>
public class UsersController(ISender sender) : CustomControllerBase
{
    /// <summary>
    /// Obtiene todos los usuarios.
    /// </summary>
    /// <returns>Una lista de usuarios.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll(string filterRequest)
    {
        var query = new GetAllUsersQuery(filterRequest);
        var response = await sender.Send(request: query);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Autentica a un usuario.
    /// </summary>
    /// <param name="request">Los datos necesarios para la autenticación.</param>
    /// <returns>Una respuesta con el resultado de la autenticación.</returns>
    [HttpPost(template: "Authenticate")]
    public async Task<ActionResult<ServiceResponse<AuthenticationResponse>>> Authenticate([FromBody] AuthenticationRequest request)
    {
        var command = new AuthenticateUserCommand(request, IpAddress: GenerateIpAddress());
        var response = await sender.Send(command);
        
        return Ok(response);
    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="request">Los datos necesarios para crear un usuario.</param>
    /// <returns>El resultado de la creación del usuario.</returns>
    // [Authorize(Roles = "Administrator")]
    [HttpPost(template: "Create")]
    public async Task<IActionResult> Create([FromBody] UserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateUserCommand(request);
        var response = await sender.Send(request: command);
        
        return response.Result.Match(
            onValue: unit => Ok(unit),
            onError: Problem);
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="request">Los datos necesarios para actualizar el usuario.</param>
    /// <returns>El resultado de la actualización del usuario.</returns>
    [HttpPut(template: "Update")]
    public async Task<IActionResult> Update([FromBody] UserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var command = new UpdateUserCommand(request);
        var response = await sender.Send(request: command);
        
        return response.Result.Match(
            onValue: unit => Ok(unit),
            onError: Problem);
    }

    /// <summary>
    /// Elimina un usuario por ID.
    /// </summary>
    /// <param name="id">El identificador del usuario a eliminar.</param>
    /// <returns>El resultado de la eliminación del usuario.</returns>
    [HttpDelete(template: "Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteUserCommand(id);
        var response = await sender.Send(request: command);
        
        return response.Result.Match(
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