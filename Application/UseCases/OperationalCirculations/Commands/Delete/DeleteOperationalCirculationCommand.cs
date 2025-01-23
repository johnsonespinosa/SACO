using Application.Interfaces;

namespace Application.UseCases.OperationalCirculations.Commands.Delete;

public record DeleteOperationalCirculationCommand(Guid Id) : IRequest<ErrorOr<Unit>>
{
    internal sealed class DeleteOperationalCirculationCommandHandler(IOperationalCirculationService service) : IRequestHandler<DeleteOperationalCirculationCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(DeleteOperationalCirculationCommand request, CancellationToken cancellationToken)
        {
            return await service.Delete(request.Id);
        }
    }
}