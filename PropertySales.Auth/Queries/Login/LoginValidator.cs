using FluentValidation;

namespace PropertySales.SecureAuth.Queries.Login;

public class LoginValidator : AbstractValidator<LoginQuery>
{
    public LoginValidator()
    {
        RuleFor(q => q.Password).MinimumLength(6).NotEmpty();
        RuleFor(q => q.Email).EmailAddress().NotEmpty().MaximumLength(255);
    }
}