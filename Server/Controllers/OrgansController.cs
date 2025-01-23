using Api.Controllers.Bases;
using Application.UseCases.Organs.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrgansController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var readOnlyCollection = await sender.Send(request: new GetAllOrgansQuery());
        return Ok(readOnlyCollection);
    }
}