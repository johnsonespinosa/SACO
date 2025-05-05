using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Bases;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[Route(template: "/error")]
public class ErrorsController(ILogger<ErrorsController> logger) : ControllerBase
{
    public IActionResult Error()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        if (exceptionFeature != null)
        {
            var exception = exceptionFeature.Error;

            // Crear un objeto ProblemDetails con más información
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ocurrió un error al procesar su solicitud.",
                Detail = exception.Message,
                Instance = HttpContext.Request.Path
            };

            logger.LogError(exception, message: "Ocurrió una excepción no controlada.");

            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }

        return Problem();
    }
}