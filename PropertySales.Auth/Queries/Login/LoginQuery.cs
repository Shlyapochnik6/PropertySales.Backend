using MediatR;

namespace PropertySales.SecureAuth.Queries.Login;

public class LoginQuery : IRequest<UserDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
