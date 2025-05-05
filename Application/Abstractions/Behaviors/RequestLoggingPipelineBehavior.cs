using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Application.Abstractions.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        try
        {
            logger.LogInformation("Procesando solicitud {RequestName}", requestName);

            var result = await next(cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Solicitud completada {RequestName}", requestName);
            }
            else
            {
                using (LogContext.PushProperty("Errors", result.Errors, destructureObjects: true))
                {
                    logger.LogError(
                        "Solicitud fallida {RequestName} con {ErrorCount} errores: {ErrorCodes}",
                        requestName,
                        result.Errors.Count,
                        string.Join(", ", result.Errors.Select(e => e.Code)));
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error al procesar la solicitud {RequestName}", requestName);
            throw;
        }
    }
}