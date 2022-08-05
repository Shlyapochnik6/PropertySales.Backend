using MediatR;

namespace PropertySales.SecureAuth.Commands.Registration;

public class RegistrationCommand : IRequest<AuthenticatedResponse>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
