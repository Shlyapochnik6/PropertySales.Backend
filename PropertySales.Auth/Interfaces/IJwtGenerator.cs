using PropertySales.Domain;
using System.Security.Claims;

namespace PropertySales.SecureAuth.Interfaces;

public interface IJwtGenerator 
{ 
    string CreateToken(User user);
    string CreateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
