using Microsoft.AspNetCore.Mvc;
using SACO.Application.Common.Interfaces;
using SACO.Application.Models;
using SACO.Domain.Enums;

namespace SACO.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CirculationsController(ICirculationService circulationService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CirculationDto>>> GetCirculations(
        [FromQuery] string? firstName,
        [FromQuery] string? lastName,
        [FromQuery] DateTime? birthDate,
        [FromQuery] string? status)
    {
        try
        {
            CirculationStatus? statusEnum = null;
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<CirculationStatus>(status, out var parsedStatus))
            {
                statusEnum = parsedStatus;
            }

            var circulations = await circulationService.SearchCirculationsAsync(firstName, lastName, birthDate, statusEnum);
            return Ok(circulations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving circulations: {ex.Message}");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CirculationDto>> GetCirculation(Guid id)
    {
        try
        {
            var circulation = await circulationService.GetCirculationByIdAsync(id);
            if (circulation == null) return NotFound();
            return Ok(circulation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving circulation: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CirculationDto>> CreateCirculation(CreateCirculationDto createDto)
    {
        try
        {
            // TODO: Obtener userId del contexto de seguridad
            var testUserId = Guid.NewGuid();
            var circulation = await circulationService.CreateOperativeCirculationAsync(createDto, testUserId);
            return CreatedAtAction(nameof(GetCirculation), new { id = circulation.Id }, circulation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creating circulation: {ex.Message}");
        }
    }

    [HttpPut("{id:guid}/validate")]
    public async Task<ActionResult<CirculationDto>> ValidateCirculation(Guid id)
    {
        try
        {
            // TODO: Obtener userId del contexto de seguridad
            var testUserId = Guid.NewGuid();
            var circulation = await circulationService.ValidateCirculationAsync(id, testUserId);
            return Ok(circulation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error validating circulation: {ex.Message}");
        }
    }

    [HttpPut("{id:guid}/reject")]
    public async Task<ActionResult<CirculationDto>> RejectCirculation(Guid id, [FromBody] ValidateCirculationDto rejectDto)
    {
        try
        {
            // TODO: Obtener userId del contexto de seguridad
            var testUserId = Guid.NewGuid();
            var circulation = await circulationService.RejectCirculationAsync(id, testUserId, rejectDto.Reason);
            return Ok(circulation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error rejecting circulation: {ex.Message}");
        }
    }
}