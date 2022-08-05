using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.SecureAuth;

public class AuthenticatedResponse
{
    public long Id { get; set; }
    public string UserName { set; get; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}