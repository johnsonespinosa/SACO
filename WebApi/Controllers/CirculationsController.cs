using Application.Features.Commands.Circulations.Create;
using Application.Features.Commands.Circulations.Delete;
using Application.Features.Commands.Circulations.Update;
using Application.Features.Queries.Circulations.GetCirculationById;
using Application.Features.Queries.Circulations.GetCirculations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Bases;

namespace WebApi.Controllers;

[Route(template: "api/circulations")]
[Authorize(Policy = "CirculationManagement")]
public class CirculationsController(ISender sender) : CustomController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetCirculationQuery query)
    {
        var result = await sender.Send(query);
        return result.IsSuccess  ? Ok(result.Value) : BadRequest(result.Errors);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await sender.Send(new GetCirculationByIdQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCirculationCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value) 
            : BadRequest(result.Errors);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id, 
        [FromBody] UpdateCirculationCommand command)
    {
        var result = await sender.Send(command);
        return result.IsSuccess ? NoContent() : BadRequest(result.Errors);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await sender.Send(new DeleteCirculationCommand(id));
        return result.IsSuccess  ? NoContent() : BadRequest(result.Errors);
    }
}