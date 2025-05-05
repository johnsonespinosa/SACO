using Domain.Models;
using MediatR;

namespace Application.Abstractions.Interfaces.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;