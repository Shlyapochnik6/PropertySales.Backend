using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Role.Commands.SetRole;

public class SetRoleCommandValidator : AbstractValidator<SetRoleCommand>
{
    public SetRoleCommandValidator()
    {
        RuleFor(role => role.UserId).NotEmpty();
        RuleFor(role => role.RoleId).NotEmpty();
    }
}