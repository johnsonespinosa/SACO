using Application.DTOs.OperationalCirculations;
using Application.Interfaces;

namespace Application.UseCases.OperationalCirculations.Commands.Create;

public record CreateOperationalCirculationCommand(CreateOperationalCirculationRequest CreateOperationalCirculationRequest) : IRequest<ErrorOr<Unit>>
{
    internal sealed class CreateOperationalCirculationCommandHandler(IOperationalCirculationService service) : IRequestHandler<CreateOperationalCirculationCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(CreateOperationalCirculationCommand request, CancellationToken cancellationToken)
        {
            return await service.Add(request.CreateOperationalCirculationRequest);
        }
    }
}