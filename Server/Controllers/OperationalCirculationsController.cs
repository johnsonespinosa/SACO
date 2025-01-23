using Api.Controllers.Bases;
using Application.DTOs.OperationalCirculations;
using Application.UseCases.OperationalCirculations.Commands.Create;
using Application.UseCases.OperationalCirculations.Commands.Delete;
using Application.UseCases.OperationalCirculations.Commands.Update;
using Application.UseCases.OperationalCirculations.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OperationalCirculationsController(ISender sender) : CustomControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(string filterRequest)
    {
        var readOnlyCollection = await sender.Send(request: new GetAllOperationalCirculationQuery(filterRequest));
        return Ok(readOnlyCollection);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOperationalCirculationRequest commandRequest)
    {
        var result = await sender.Send(request: new CreateOperationalCirculationCommand(commandRequest));
        return result.Match(
            onValue: operationalCirculation => Ok(),
            onError: errors => Problem(errors));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOperationalCirculationRequest commandRequest)
    {
        var result = await sender.Send(request: new UpdateOperationalCirculationCommand(commandRequest));
        return result.Match(
            onValue: operationalCirculation => Ok(),
            onError: errors => Problem(errors));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await sender.Send(request: new DeleteOperationalCirculationCommand(id));
        return result.Match(
            onValue: operationalCirculation => Ok(),
            onError: errors => Problem(errors));
    }
}