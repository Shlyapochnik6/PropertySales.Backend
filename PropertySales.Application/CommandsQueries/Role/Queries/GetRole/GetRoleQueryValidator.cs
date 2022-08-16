using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Role.Queries.GetRole;

public class GetRoleQueryValidator : AbstractValidator<GetRoleQuery>
{
    public GetRoleQueryValidator()
    {
        RuleFor(role => role.RoleId).NotEmpty();
    }
}