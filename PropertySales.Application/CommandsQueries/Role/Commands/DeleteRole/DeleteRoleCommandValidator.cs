using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Role.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(user => user.RoleId).NotEmpty();
    }
}