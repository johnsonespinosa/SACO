using Api.Controllers.Bases;
using Application.UseCases.Citizenships.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CitizenshipsController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var readOnlyCollection = await sender.Send(request: new GetAllCitizenshipsQuery());
        return Ok(readOnlyCollection);
    }
}