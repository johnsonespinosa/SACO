using Application.DTOs.Expirations;
using Application.Interfaces;

namespace Application.UseCases.Expirations.Queries.GetAll;

public record GetAllExpirationsQuery : IRequest<IReadOnlyCollection<ExpirationResponse>>
{
    internal sealed class GetAllExpirationsQueryHandler(IExpirationService service) : IRequestHandler<GetAllExpirationsQuery, IReadOnlyCollection<ExpirationResponse>>
    {
        public async Task<IReadOnlyCollection<ExpirationResponse>> Handle(GetAllExpirationsQuery request, CancellationToken cancellationToken)
        {
            return await service.GetAll();
        }
    }
}