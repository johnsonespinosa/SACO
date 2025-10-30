using Microsoft.AspNetCore.Mvc;
using SACO.Application.Common.Interfaces;
using SACO.Domain.Entities;

namespace SACO.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SeedController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<ActionResult> SeedData()
    {
        try
        {
            // Crear órgano de prueba
            var organ = new Organ 
            { 
                Name = "Dirección de Inmigración y Extranjería", 
                Code = "DIIE",
            };
            await _unitOfWork.Organs.AddAsync(organ);

            // Crear departamento de prueba
            var department = new Department
            {
                Name = "Departamento de Control Migratorio",
                Code = "CM",
                OrganId = organ.Id
            };
            await _unitOfWork.Departments.AddAsync(department);

            // Crear pasajero de prueba
            var passenger = new Passenger
            {
                FirstName = "Juan",
                FirstLastName = "Pérez",
                BirthDate = new DateTime(1985, 5, 15),
                Citizenship = "Cuba"
            };
            passenger.CalculateSearchKey();
            await _unitOfWork.Passengers.AddAsync(passenger);

            await _unitOfWork.SaveChangesAsync();

            return Ok("Datos de prueba creados exitosamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error creando datos de prueba: {ex.Message}");
        }
    }
}