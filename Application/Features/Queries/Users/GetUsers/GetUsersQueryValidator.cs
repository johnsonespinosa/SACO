using Domain.Errors;
using FluentValidation;

namespace Application.Features.Queries.Users.GetUsers;

public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersQueryValidator()
    {
        RuleFor(expression: query => query.PageNumber)
            .GreaterThan(0)
            .WithErrorCode(DomainErrors.General.InvalidPageNumber.Code);

        RuleFor(expression: query => query.PageSize)
            .InclusiveBetween(1, 100)
            .WithErrorCode(DomainErrors.General.InvalidPageSize.Code);
    }
}