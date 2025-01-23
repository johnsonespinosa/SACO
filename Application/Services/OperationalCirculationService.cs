using Application.DTOs.OperationalCirculations;
using Application.Interfaces;
using Application.Models;
using Application.Specifications;
using Domain.Entities;

namespace Application.Services;

public class OperationalCirculationService(IRepositoryAsync<OperationalCirculation> repositoryAsync, IMapper mapper)
    : IOperationalCirculationService
{
    public async Task<ErrorOr<Unit>> Add(CreateOperationalCirculationRequest request)
    {
        var operationalCirculation = mapper.Map<OperationalCirculation>(request);
        await repositoryAsync.AddAsync(operationalCirculation);
        return Unit.Value;
    }

    public async Task<ErrorOr<Unit>> Update(UpdateOperationalCirculationRequest request)
    {
        var operationalCirculation = await repositoryAsync.GetByIdAsync(request.Id);
        if (operationalCirculation is null)
            return Error.NotFound(code: "UpdateOperationalCirculation.NotFound",
                description: ResponseMessages.ReadNotFound);

        mapper.Map(operationalCirculation, request);
        await repositoryAsync.UpdateAsync(operationalCirculation);
        return Unit.Value;
    }

    public async Task<ErrorOr<Unit>> Delete(Guid id)
    {
        var operationalCirculation = await repositoryAsync.GetByIdAsync(id);
        if (operationalCirculation is null)
            return Error.NotFound(code: "UpdateOperationalCirculation.NotFound",
                description: ResponseMessages.ReadNotFound);
        await repositoryAsync.DeleteAsync(operationalCirculation);
        return Unit.Value;
    }

    public async Task<IReadOnlyCollection<OperationalCirculationResponse>> GetAll(string filterRequest)
    {
        var specification = new GetAllOperationalCirculationsSpecification(filterRequest);
        var operationalCirculations = await repositoryAsync.ListAsync(specification);
        var operationalCirculationsMapped = mapper.Map<List<OperationalCirculationResponse>>(operationalCirculations);
        return operationalCirculationsMapped;
    }
}