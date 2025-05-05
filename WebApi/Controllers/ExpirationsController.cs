using Application.Features.Queries.Expirations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Bases;

namespace WebApi.Controllers;

[Route(template: "api/expirations")]
public class ExpirationsController(ISender sender) : CustomController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(request: new GetExpirationsQuery());
        return result.IsSuccess  ? Ok(result.Value) : BadRequest(result.Errors);
    }
}