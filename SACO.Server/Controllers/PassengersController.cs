using Microsoft.AspNetCore.Mvc;
using SACO.Application.Common.Interfaces;
using SACO.Shared.Models;

namespace SACO.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassengersController(IPassengerService passengerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PassengerResponse>>> GetPassengers(
        [FromQuery] string? firstName,
        [FromQuery] string? lastName,
        [FromQuery] DateTime? birthDate)
    {
        try
        {
            var passengers = await passengerService.SearchPassengersAsync(firstName, lastName, birthDate);
            return Ok(passengers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving passengers: {ex.Message}");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PassengerResponse>> GetPassenger(Guid id)
    {
        try
        {
            var passenger = await passengerService.GetPassengerWithCirculationsAsync(id);
            if (passenger == null) return NotFound();
            return Ok(passenger);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving passenger: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<PassengerResponse>> CreatePassenger(CreatePassengerRequest createDto)
    {
        try
        {
            var passenger = await passengerService.CreatePassengerAsync(createDto);
            return CreatedAtAction(nameof(GetPassenger), new { id = passenger.Id }, passenger);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creating passenger: {ex.Message}");
        }
    }
}