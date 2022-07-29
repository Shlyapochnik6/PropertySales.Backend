using PropertySales.Domain;

namespace PropertySales.SecureAuth.Interfaces;

public interface IJwtGenerator 
{ 
    string CreateToken(User user);
}
