using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using AutoMapper;
using Domain.Errors;
using Domain.Models;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Queries.Users.GetUserById;

internal sealed class GetUserByIdQueryHandler(
    UserManager<User> userManager,
    IMapper mapper)
    : IQueryHandler<GetUserByIdQuery, UserDto>
{
    public async Task<Result<UserDto>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Buscar usuario por ID
        var user = await userManager.FindByIdAsync(request.Id);
        if (user is null)
            return Result.Failure<UserDto>(DomainErrors.User.NotFound);

        // Mapear a DTO de respuesta
        var userResponse = mapper.Map<UserDto>(user);
        
        return Result.Success(userResponse);
    }
}