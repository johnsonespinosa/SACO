using Ardalis.Specification;
using AutoMapper;
using SACO.Application.Common.Interfaces;
using SACO.Application.Common.Specifications;
using SACO.Domain.Entities;
using SACO.Domain.Enums;
using SACO.Domain.ValueObjects;
using SACO.Shared.Models;

namespace SACO.Application.Services;

public class CirculationService : ICirculationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CirculationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CirculationResponse> CreateOperativeCirculationAsync(CreateCirculationRequest createDto, Guid userId)
    {
        // Mapear DTO a entidad
        var circulation = _mapper.Map<Circulation>(createDto);
        
        // Generar número de expediente operativo
        var lastOpCirculation = await _unitOfWork.Circulations.FirstOrDefaultAsync(
            new CirculationWithDetailsSpecification(status: CirculationStatus.Operative));
        
        var nextSequence = 1;
        if (lastOpCirculation != null)
        {
            nextSequence = lastOpCirculation.ExpeditionSequence + 1;
        }
        
        circulation.ExpeditionNumber = ExpeditionNumber.CreateOperative(nextSequence).ToString();
        circulation.Status = CirculationStatus.Operative;
        circulation.CreatorUserId = userId;
        circulation.SetCreationAudit(userId.ToString());
        circulation.CalculateTraceKey();

        // Verificar si ya existe circulación activa para el pasajero
        var existingCirculations = await _unitOfWork.Circulations.ListAsync(
            new CirculationWithDetailsSpecification());
            
        var activeCirculation = existingCirculations.FirstOrDefault(c => 
            c.PassengerId == circulation.PassengerId && 
            (c.Status == CirculationStatus.Operative || c.Status == CirculationStatus.Effective) &&
            !c.IsExpired);
            
        if (activeCirculation != null)
        {
            // Podríamos lanzar un evento de dominio aquí
            // "Este pasajero ya está circulado, consultar a la DIIE"
        }

        await _unitOfWork.Circulations.AddAsync(circulation);
        await _unitOfWork.SaveChangesAsync();
        
        // Cargar relaciones para el DTO
        await LoadCirculationRelations(circulation);
        return _mapper.Map<CirculationResponse>(circulation);
    }

    public async Task<CirculationResponse> ValidateCirculationAsync(Guid circulationId, Guid validatorUserId)
    {
        var circulation = await _unitOfWork.Circulations.GetByIdAsync(circulationId);
        if (circulation == null)
            throw new ArgumentException("Circulation not found");

        // Generar número de expediente efectivo
        var lastEfCirculation = await _unitOfWork.Circulations.FirstOrDefaultAsync(
            new CirculationWithDetailsSpecification(status: CirculationStatus.Effective));
        
        var nextSequence = 1;
        if (lastEfCirculation != null)
        {
            nextSequence = lastEfCirculation.ExpeditionSequence + 1;
        }
        
        var effectiveExpeditionNumber = ExpeditionNumber.CreateEffective(nextSequence);
        circulation.MarkAsEffective(validatorUserId, effectiveExpeditionNumber.ToString());
        
        await _unitOfWork.Circulations.UpdateAsync(circulation);
        await _unitOfWork.SaveChangesAsync();
        
        await LoadCirculationRelations(circulation);
        return _mapper.Map<CirculationResponse>(circulation);
    }

    public async Task<CirculationResponse> RejectCirculationAsync(Guid circulationId, Guid validatorUserId, string reason)
    {
        var circulation = await _unitOfWork.Circulations.GetByIdAsync(circulationId);
        if (circulation == null)
            throw new ArgumentException("Circulation not found");

        circulation.MarkAsRejected(validatorUserId, reason);
        await _unitOfWork.Circulations.UpdateAsync(circulation);
        await _unitOfWork.SaveChangesAsync();
        
        await LoadCirculationRelations(circulation);
        return _mapper.Map<CirculationResponse>(circulation);
    }

    public async Task<IEnumerable<CirculationResponse>> SearchCirculationsAsync(string? firstName, string? lastName, DateTime? birthDate, CirculationStatus? status)
    {
        var spec = new CirculationWithDetailsSpecification(status);
        var circulations = await _unitOfWork.Circulations.ListAsync(spec);
        
        // Aplicar filtros adicionales si se proporcionan
        if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName) || birthDate.HasValue)
        {
            circulations = circulations.Where(c =>
                (string.IsNullOrEmpty(firstName) || c.Passenger.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(lastName) || c.Passenger.FirstLastName.Contains(lastName, StringComparison.OrdinalIgnoreCase)) &&
                (!birthDate.HasValue || c.Passenger.BirthDate.Date == birthDate.Value.Date)
            ).ToList();
        }
        
        return _mapper.Map<IEnumerable<CirculationResponse>>(circulations);
    }

    public async Task<CirculationResponse?> GetCirculationByIdAsync(Guid circulationId)
    {
        var spec = new CirculationWithDetailsSpecification();
        spec.Query.Where(c => c.Id == circulationId);
        
        var circulation = await _unitOfWork.Circulations.FirstOrDefaultAsync(spec);
        return circulation != null ? _mapper.Map<CirculationResponse>(circulation) : null;
    }

    public async Task CheckExpiredCirculationsAsync()
    {
        var operativeCirculations = await _unitOfWork.Circulations.ListAsync(
            new CirculationWithDetailsSpecification(status: CirculationStatus.Operative));
            
        foreach (var circulation in operativeCirculations)
        {
            circulation.CheckExpiration();
            if (circulation.Status == CirculationStatus.Expired)
            {
                await _unitOfWork.Circulations.UpdateAsync(circulation);
            }
        }
        
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task LoadCirculationRelations(Circulation circulation)
    {
        // Cargar relaciones si no están cargadas
        if (circulation.Passenger == null)
        {
            circulation.Passenger = await _unitOfWork.Passengers.GetByIdAsync(circulation.PassengerId);
        }
        if (circulation.Organ == null)
        {
            circulation.Organ = await _unitOfWork.Organs.GetByIdAsync(circulation.OrganId);
        }
        if (circulation.Department == null)
        {
            circulation.Department = await _unitOfWork.Departments.GetByIdAsync(circulation.DepartmentId);
        }
    }
}