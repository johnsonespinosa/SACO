using Application.Features.Queries.Organs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Bases;

namespace WebApi.Controllers;

[Route(template: "api/organs")]
public class OrgansController(ISender sender) : CustomController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(request: new GetOrgansQuery());
        return result.IsSuccess  ? Ok(result.Value) : BadRequest(result.Errors);
    }
}