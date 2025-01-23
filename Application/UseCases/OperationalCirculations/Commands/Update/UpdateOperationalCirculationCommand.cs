using Application.DTOs.OperationalCirculations;
using Application.Interfaces;

namespace Application.UseCases.OperationalCirculations.Commands.Update;

public record UpdateOperationalCirculationCommand(UpdateOperationalCirculationRequest OperationalCirculationRequest) : IRequest<ErrorOr<Unit>>
{
    internal sealed class UpdateOperationalCirculationCommandHandler(IOperationalCirculationService service) : IRequestHandler<UpdateOperationalCirculationCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(UpdateOperationalCirculationCommand request, CancellationToken cancellationToken)
        {
            return await service.Update(request.OperationalCirculationRequest);
        }
    }
}