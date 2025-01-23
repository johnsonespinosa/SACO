namespace Application.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            // Registra la excepción con información relevante
            _logger.LogError(ex, message: "Sistema Automatizado de Circulaciones Operativas: Excepción no controlada para solicitud {Name} {@Request}", requestName, request);

            throw; // Re-lanza la excepción después del registro
        }
    }
}