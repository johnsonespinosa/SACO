using Shared.DTOs;
using Shared.Interfaces;

namespace Application.UseCases.OperationalCirculations.Commands.Delete;

public record DeleteOperationalCirculationCommand(Guid Id) : IRequest<ServiceResponse<ErrorOr<Unit>>>
{
    internal sealed class DeleteOperationalCirculationCommandHandler(ICirculationService service) : IRequestHandler<DeleteOperationalCirculationCommand, ServiceResponse<ErrorOr<Unit>>>
    {
        public async Task<ServiceResponse<ErrorOr<Unit>>> Handle(DeleteOperationalCirculationCommand request, CancellationToken cancellationToken)
        {
            return await service.Delete(request.Id);
        }
    }
}