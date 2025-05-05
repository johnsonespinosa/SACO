using Application.Abstractions.DTOs;
using Application.Features.Commands.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Bases;

namespace WebApi.Controllers;

[Route("api/auth")]
[AllowAnonymous]
public class AuthController(ISender sender, ILogger<AuthController> logger) : CustomController
{
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
    {
        logger.LogInformation("Solicitud de refresh token recibida");
        
        var command = new RefreshTokenCommand(
            request.AccessToken,
            request.RefreshToken,
            GetIpAddress());

        var result = await sender.Send(command);

        if (result.IsSuccess)
        {
            logger.LogInformation("Refresh token exitoso");
            return Ok(result.Value);
        }

        logger.LogWarning("Refresh token fallido: {Errors}", string.Join(", ", result.Errors));
        return BadRequest(result.Errors);
    }
    
    [HttpPost]
    public async Task<IActionResult> Authenticate(AuthRequest request)
    {
        logger.LogInformation("Intento de autenticación para usuario: {Username}", request.UserName);
        
        var command = new AuthenticationCommand(
            request.UserName,
            request.Password,
            GetIpAddress());

        var result = await sender.Send(command);

        if (result.IsSuccess)
        {
            logger.LogInformation("Autenticación exitosa para usuario: {Username}", request.UserName);
            return Ok(result.Value);
        }

        logger.LogWarning("Autenticación fallida para usuario: {Username}", request.UserName);
        return BadRequest(result.Errors);
    }
    
    private string GetIpAddress()
    {
        var ipAddress = Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "IP desconocida";
        
        logger.LogDebug("IP detectada: {IpAddress}", ipAddress);
        return ipAddress;
    }
}