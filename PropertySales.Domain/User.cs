using Microsoft.AspNetCore.Identity;

namespace PropertySales.Domain;

public class User : IdentityUser<long>
{
    public decimal Balance { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public List<Purchase> Purchases { get; set; }
}