using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Role.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(role => role.RoleId).NotEmpty();
        RuleFor(role => role.Name).NotEmpty().MaximumLength(75);
    }
}