using Domain.Errors;
using FluentValidation;

namespace Application.Features.Queries.Users.GetUserById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(expression: query => query.Id)
            .NotEmpty()
            .WithErrorCode(DomainErrors.User.IdRequired.Code)
            .WithMessage(DomainErrors.User.IdRequired.Detail);
    }
}