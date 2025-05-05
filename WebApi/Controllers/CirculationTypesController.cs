using Application.Features.Queries.CirculationTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Bases;

namespace WebApi.Controllers;

[Route(template: "api/circulation-Types")]
public class CirculationTypesController(ISender sender) : CustomController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(request: new GetCirculationTypesQuery());
        return result.IsSuccess  ? Ok(result.Value) : BadRequest(result.Errors);
    }
}