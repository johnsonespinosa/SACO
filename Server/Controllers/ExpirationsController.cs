using Api.Controllers.Bases;
using Application.UseCases.Expirations.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ExpirationsController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var readOnlyCollection = await sender.Send(request: new GetAllExpirationsQuery());
        return Ok(readOnlyCollection);
    }
}