using Api.Controllers.Bases;
using Application.UseCases.Circulations.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CirculationsController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var readOnlyCollection = await sender.Send(request: new GetAllCirculationsQuery());
        return Ok(readOnlyCollection);
    }
}