using Shared.Exceptions;

namespace Client.Middlewares;

public class ErrorHandler(ILogger<ErrorHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Lógica antes de enviar la solicitud
        logger.LogInformation(message: "Enviando solicitud a {Url}", request.RequestUri);

        HttpResponseMessage response;

        try
        {
            response = await base.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            // Manejo de errores de red
            logger.LogError(ex, message: "Error al enviar la solicitud a {Url}", request.RequestUri);
            throw new CustomHttpException(message: "Error al enviar la solicitud", ex);
        }

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var errorMessage = $"Error: {response.StatusCode}, Content: {errorContent}";

            // Registro del error
            logger.LogError(errorMessage);

            // Lanzar una excepción personalizada con más contexto
            throw new CustomHttpException(message: $"Error en la solicitud: {response.StatusCode}", errorContent);
        }

        return response;
    }
}
