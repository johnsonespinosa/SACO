using Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Commons.Http;

namespace WebApi.Controllers.Bases;

[ApiController]
public class CustomController : ControllerBase
{
    /// <summary>
    /// Devuelve un resultado de error basado en una lista de errores.
    /// </summary>
    /// <param name="errors">Lista de errores a procesar.</param>
    /// <returns>Un IActionResult que representa el resultado del error.</returns>
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
            return Problem();

        if (errors.All(error => error.Type == ErrorType.Validation))
            return ValidationProblem(errors);

        HttpContext.Items[HttpContextItemKey.Error] = errors;

        return Problem(errors[0]);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };
        return Problem(statusCode: statusCode, title: error.Detail);
    }


    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(key: error.Code, errorMessage: error.Detail);
        }

        return ValidationProblem(modelStateDictionary);
    }
}