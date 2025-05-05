using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middleware;

public class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger,
    IWebHostEnvironment environment)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Excepción no controlada");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var problemDetails = new ProblemDetails
        {
            Title = "Error interno del servidor",
            Status = (int)statusCode,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Instance = context.Request.Path,
            Detail = environment.IsDevelopment() ? exception.ToString() : null // ✅ Usar environment
        };

        if (exception is BadHttpRequestException)
        {
            statusCode = HttpStatusCode.BadRequest;
            problemDetails.Title = "Solicitud inválida";
            problemDetails.Status = (int)statusCode;
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        }

        problemDetails.Extensions["correlationId"] = context.Items["CorrelationId"];
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";
    
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}