using System.Diagnostics;
using Api.Commons.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Commons.Errors;

public class ProblemDetailFactory(ApiBehaviorOptions options) : ProblemDetailsFactory
{
    public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string? title = null,
        string? type = null, string? detail = null, string? instance = null)
    {
        statusCode ??= 500;
        var problemDetail = new ProblemDetails()
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };
        
        ApplyDefaultProblemDetail(httpContext, problemDetail, statusCode.Value);
        
        return problemDetail;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
        ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null, string? type = null,
        string? detail = null, string? instance = null)
    {
        if (modelStateDictionary == null)
            throw new ArgumentNullException(nameof(modelStateDictionary));
        
        statusCode ??= 400;
        
        var problemDetail = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        if (title != null)
            problemDetail.Title = title;

        ApplyDefaultProblemDetail(httpContext, problemDetail, statusCode.Value);
        
        return problemDetail;
    }

    private void ApplyDefaultProblemDetail(HttpContext context, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;
        if (options.ClientErrorMapping.TryGetValue(statusCode, out var claimErrorData))
        {
            problemDetails.Title ??= claimErrorData.Title;
            problemDetails.Type ??= claimErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        problemDetails.Extensions["traceId"] = traceId;

        if (context.Items[HttpContextItemKey.Error] is List<Error> errors)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(error => error.Code));
        }
    }
}