using MediatR;

namespace PropertySales.SecureAuth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthenticatedResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
