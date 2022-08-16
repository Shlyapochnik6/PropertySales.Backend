using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Role.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(role => role.Name).NotEmpty().MaximumLength(75);
    }   
}