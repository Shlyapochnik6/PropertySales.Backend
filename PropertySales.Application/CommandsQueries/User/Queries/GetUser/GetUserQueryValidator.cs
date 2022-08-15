using FluentValidation;

namespace PropertySales.Application.CommandsQueries.User.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(user => user.UserId).NotEmpty();
    }
}