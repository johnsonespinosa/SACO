using Shared.DTOs;
using Shared.DTOs.OperationalCirculations;
using Shared.Interfaces;

namespace Application.UseCases.OperationalCirculations.Commands.Update;

public record UpdateOperationalCirculationCommand(CirculationRequest OperationalCirculationRequest) : IRequest<ServiceResponse<ErrorOr<Unit>>>
{
    internal sealed class UpdateOperationalCirculationCommandHandler(ICirculationService service) : IRequestHandler<UpdateOperationalCirculationCommand, ServiceResponse<ErrorOr<Unit>>>
    {
        public async Task<ServiceResponse<ErrorOr<Unit>>> Handle(UpdateOperationalCirculationCommand request, CancellationToken cancellationToken)
        {
            return await service.Update(request.OperationalCirculationRequest);
        }
    }
}