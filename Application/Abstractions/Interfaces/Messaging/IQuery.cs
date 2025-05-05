using Domain.Models;
using MediatR;

namespace Application.Abstractions.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;