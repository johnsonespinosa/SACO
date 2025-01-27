using Api.Controllers.Bases;
using Application.UseCases.OperationalCirculations.Commands.Create;
using Application.UseCases.OperationalCirculations.Commands.Delete;
using Application.UseCases.OperationalCirculations.Commands.Update;
using Application.UseCases.OperationalCirculations.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.OperationalCirculations;

namespace Api.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class OperationalCirculationsController(ISender sender) : CustomControllerBase
{
    /// <summary>
    /// Obtiene todas las circulaciones operativas.
    /// </summary>
    /// <param name="filterRequest">Opcional: filtro para las circulaciones operativas.</param>
    /// <returns>Una colección de circulaciones operativas.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CirculationResponse>>> GetAll(string filterRequest)
    {
        var query = new GetAllOperationalCirculationQuery(filterRequest);
        var readOnlyCollection = await sender.Send(request: query);
        return Ok(readOnlyCollection);
    }

    /// <summary>
    /// Crea una nueva circulación operativa.
    /// </summary>
    /// <param name="request">Los datos necesarios para crear una circulación operativa.</param>
    /// <returns>Resultado de la creación.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CirculationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateOperationalCirculationCommand(request);
        var response = await sender.Send(request: command);
        return response.Result.Match(
            onValue: unit => Ok(unit),
            onError: Problem);
    }

    /// <summary>
    /// Actualiza una circulación operativa existente.
    /// </summary>
    /// <param name="request">Los datos necesarios para actualizar la circulación operativa.</param>
    /// <returns>Resultado de la actualización.</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CirculationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new UpdateOperationalCirculationCommand(request);
        var response = await sender.Send(request: command);
        return response.Result.Match(
            onValue: unit => Ok(unit),
            onError: Problem);
    }

    /// <summary>
    /// Elimina una circulación operativa por su ID.
    /// </summary>
    /// <param name="id">El ID de la circulación operativa a eliminar.</param>
    /// <returns>Resultado de la eliminación.</returns>
    [HttpDelete(template: "{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteOperationalCirculationCommand(id);
        var response = await sender.Send(request: command);
        return response.Result.Match(
            onValue: unit => Ok(unit),
            onError: Problem);
    }
}
