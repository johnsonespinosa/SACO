using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Domain.Models;

namespace Application.Features.Queries.Users.GetUsers;

public record GetUsersQuery(
    string? Filter,
    int PageNumber,
    int PageSize) : IQuery<PaginatedResult<UserDto>>;