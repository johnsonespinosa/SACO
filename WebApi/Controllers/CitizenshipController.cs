using Application.Features.Queries.Citizenships;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Bases;

namespace WebApi.Controllers;

[Route(template: "api/citizenship")]
public class CitizenshipController(ISender sender) : CustomController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(request: new GetCitizenshipQuery());
        return result.IsSuccess  ? Ok(result.Value) : BadRequest(result.Errors);
    }
}