using Shared.DTOs;
using Shared.DTOs.OperationalCirculations;
using Shared.Interfaces;

namespace Application.UseCases.OperationalCirculations.Commands.Create;

public record CreateOperationalCirculationCommand(CirculationRequest CirculationRequest) : IRequest<ServiceResponse<ErrorOr<Unit>>>
{
    internal sealed class CreateOperationalCirculationCommandHandler(ICirculationService service) : IRequestHandler<CreateOperationalCirculationCommand, ServiceResponse<ErrorOr<Unit>>>
    {
        public async Task<ServiceResponse<ErrorOr<Unit>>> Handle(CreateOperationalCirculationCommand request, CancellationToken cancellationToken)
        {
            return await service.CreateAsync(request.CirculationRequest);
        }
    }
}