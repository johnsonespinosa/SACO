using Application.Abstractions.DTOs;
using Application.Abstractions.Interfaces.Messaging;
using Application.Abstractions.Interfaces.Repositories;
using AutoMapper;
using Domain.Circulations;
using Domain.Errors;
using Domain.Models;

namespace Application.Features.Queries.Circulations.GetCirculations;

public sealed record GetCirculationQuery(
    string? SearchTerm,
    int PageNumber = 1,
    int PageSize = 10) : IQuery<PaginatedResult<CirculationDto>>;

