using MediatR;

namespace PropertySales.SecureAuth.Commands;

public class RegistrationCommand : IRequest<UserDto>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
