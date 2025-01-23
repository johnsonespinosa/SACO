using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

namespace Api.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Se produjo un error",
            Type = exception.GetType().Name,
            Detail = exception.Message,
            Instance = context.Request.Path // Proporciona la ruta que causó el error
        };

        switch (exception)
        {
            case DbUpdateException dbUpdateException:
                HandleDbUpdateException(dbUpdateException, problemDetails);
                break;

            case ValidationException validationException:
                Log.Error(exception, validationException.Message); // Usar Serilog para registrar el error
                problemDetails.Status = StatusCodes.Status400BadRequest;
                break;

            case NotFoundException notFoundException:
                Log.Error(exception, notFoundException.Message); // Usar Serilog para registrar el error
                problemDetails.Status = StatusCodes.Status404NotFound;
                break;

            case UnauthorizedAccessException unauthorizedAccessException:
                Log.Error(exception, unauthorizedAccessException.Message); // Usar Serilog para registrar el error
                problemDetails.Status = StatusCodes.Status401Unauthorized;
                break;

            case ArgumentNullException argumentNullException:
                Log.Error(exception, argumentNullException.Message); // Usar Serilog para registrar el error
                problemDetails.Status = StatusCodes.Status400BadRequest;
                break;

            default:
                // Registrar la excepción para seguimiento usando Serilog
                Log.Error(exception, messageTemplate: "Se produjo un error inesperado.");
                break;
        }

        return context.Response.WriteAsJsonAsync(problemDetails);
    }

    private void HandleDbUpdateException(DbUpdateException dbUpdateException, ProblemDetails problemDetails)
    {
        if (dbUpdateException.InnerException is NpgsqlException innerNpgsqlEx)
        {
            Log.Error(innerNpgsqlEx, dbUpdateException.Message); // Usar Serilog para registrar el error

            // Manejo específico para NpgsqlException
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Title = "Error de actualización de base de datos";
            problemDetails.Detail = innerNpgsqlEx.Message;

            switch (innerNpgsqlEx.ErrorCode)
            {
                case 23505: // Violación única
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Detail = "Se produjo una violación de restricción única.";
                    break;

                case 23503: // Violación de clave externa
                    problemDetails.Status = StatusCodes.Status400BadRequest; 
                    problemDetails.Detail = "Se produjo una violación de restricción de clave externa.";
                    break;

                case 23502: // No se puede insertar nulo
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Detail = "Se proporcionó un valor nulo para un campo que no acepta valores nulos.";
                    break; 
            }
        }
    }
}