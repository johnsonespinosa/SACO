using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Filters;

public class ProblemDetailsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var responses = operation.Responses;
        if (responses.All(keyValuePair => keyValuePair.Key != "400"))
            responses.Add("400", new OpenApiResponse { Description = "Bad Request" });
        
        if (responses.All(keyValuePair => keyValuePair.Key != "500"))
            responses.Add("500", new OpenApiResponse { Description = "Internal Server Error" });
    }
}