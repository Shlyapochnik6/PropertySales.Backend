using FluentValidation;

namespace PropertySales.SecureAuth.Commands.Registration;

public class RegistrationValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationValidator()
    {
        RuleFor(r => r.Password).NotEmpty().MaximumLength(10);
        RuleFor(r => r.Email).NotEmpty().MaximumLength(255);
        RuleFor(r => r.Name).NotEmpty().MaximumLength(75);
    }
}
