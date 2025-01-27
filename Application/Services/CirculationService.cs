using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Shared.DTOs;
using Shared.DTOs.OperationalCirculations;
using Shared.Interfaces;

namespace Application.Services;

public class CirculationService(IRepositoryAsync<Circulation> repositoryAsync, IMapper mapper)
    : ICirculationService
{
    public async Task<ServiceResponse<ErrorOr<Unit>>> CreateAsync(CirculationRequest request)
    {
        var operationalCirculation = mapper.Map<Circulation>(request);
        await repositoryAsync.AddAsync(operationalCirculation);
        return new ServiceResponse<ErrorOr<Unit>>(
            Result: Unit.Value, Succeeded: true, Message: ResponseMessages.CreateSuccess);
    }

    public async Task<ServiceResponse<ErrorOr<Unit>>> Update(CirculationRequest request)
    {
        var operationalCirculation = await repositoryAsync.GetByIdAsync(request.Id);
        if (operationalCirculation is null)
            return new ServiceResponse<ErrorOr<Unit>>(
                Result: Error.NotFound(code: "UpdateOperationalCirculation.NotFound",
                    description: ResponseMessages.ReadNotFound), Succeeded: false,
                Message: ResponseMessages.ReadNotFound);

        mapper.Map(operationalCirculation, request);
        await repositoryAsync.UpdateAsync(operationalCirculation);
        return new ServiceResponse<ErrorOr<Unit>>(
            Result: Unit.Value, Succeeded: true, Message: ResponseMessages.UpdateSuccess);
    }

    public async Task<ServiceResponse<ErrorOr<Unit>>> Delete(Guid id)
    {
        var operationalCirculation = await repositoryAsync.GetByIdAsync(id);
        if (operationalCirculation is null)
            return new ServiceResponse<ErrorOr<Unit>>(
                Result: Error.NotFound(code: "DeleteOperationalCirculation.NotFound",
                    description: ResponseMessages.ReadNotFound), Succeeded: false,
                Message: ResponseMessages.ReadNotFound);
        await repositoryAsync.DeleteAsync(operationalCirculation);
        return new ServiceResponse<ErrorOr<Unit>>(
            Result: Unit.Value, Succeeded: true, Message: ResponseMessages.DeleteSuccess);
    }

    public async Task<ServiceResponse<IReadOnlyCollection<CirculationResponse>>> GetAll(string filterRequest)
    {
        var specification = new GetAllOperationalCirculationsSpecification(filterRequest);
        var operationalCirculations = await repositoryAsync.ListAsync(specification);
        var operationalCirculationsMapped = mapper.Map<List<CirculationResponse>>(operationalCirculations);
        return new ServiceResponse<IReadOnlyCollection<CirculationResponse>>(
            Result: operationalCirculationsMapped, Succeeded: true, Message: ResponseMessages.DeleteSuccess);
    }
}