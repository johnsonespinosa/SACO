using Serilog.Context;

namespace WebApi.Middleware;

public class RequestContextLoggingMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeaderName = "Correlation-CirculationId";

    public Task Invoke(HttpContext context)
    {
        var correlationId = GetCorrelationId(context);
        context.Items["CorrelationId"] = correlationId; // Disponible para toda la solicitud
    
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            return next.Invoke(context);
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(
            CorrelationIdHeaderName,
            out var correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}