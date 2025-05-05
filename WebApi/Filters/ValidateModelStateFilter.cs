using Domain.Errors;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class ValidateModelStateFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, 
        ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .SelectMany(keyValuePair => keyValuePair.Value!.Errors)
                .Select(error => Error.Validation(
                    code: "Model.Validation",
                    title: "Datos inválidos",
                    detail: error.ErrorMessage))
                .ToList();
            
            context.Result = new BadRequestObjectResult(Result.Failure(errors));
            return;
        }
        
        await next();
    }
}