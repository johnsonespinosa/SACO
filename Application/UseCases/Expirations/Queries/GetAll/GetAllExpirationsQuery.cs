using Shared.DTOs;
using Shared.DTOs.Expirations;
using Shared.Interfaces;

namespace Application.UseCases.Expirations.Queries.GetAll;

public record GetAllExpirationsQuery : IRequest<ServiceResponse<IReadOnlyCollection<ExpirationResponse>>>
{
    internal sealed class GetAllExpirationsQueryHandler(IExpirationService service) : IRequestHandler<GetAllExpirationsQuery, ServiceResponse<IReadOnlyCollection<ExpirationResponse>>>
    {
        public async Task<ServiceResponse<IReadOnlyCollection<ExpirationResponse>>> Handle(GetAllExpirationsQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll();
        }
    }
}