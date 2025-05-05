using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;

namespace Application.Features.Queries.Users.GetUserById;

public record GetUserByIdQuery(string Id) : IQuery<UserDto>;