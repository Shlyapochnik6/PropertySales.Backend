using FluentValidation;

namespace PropertySales.SecureAuth.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(user => user.Id).NotEmpty();
        RuleFor(user => user.Email).NotEmpty();
        RuleFor(user => user.NewPassword).NotEmpty();
    }
}