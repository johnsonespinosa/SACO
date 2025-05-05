using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.Users.GetUsers;

internal sealed class GetUsersQueryHandler(
    UserManager<User> userManager,
    IMapper mapper) : IQueryHandler<GetUsersQuery, PaginatedResult<UserDto>>
{
    public async Task<Result<PaginatedResult<UserDto>>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        // Construir query base
        var query = userManager.Users.AsQueryable();

        // Aplicar filtro de nombre de usuario
        if (!string.IsNullOrWhiteSpace(request.Filter))
        {
            query = query.Where(user => 
                EF.Functions.Like(user.UserName, $"%{request.Filter}%"));
        }

        // Calcular total de registros
        var totalRecords = await query.CountAsync(cancellationToken);

        // Aplicar paginación y proyección
        var users = await query
            .OrderBy(user => user.UserName)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return Result.Success(PaginatedResult<UserDto>.Create(
            items: users,
            totalItems: totalRecords,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize));
    }
}