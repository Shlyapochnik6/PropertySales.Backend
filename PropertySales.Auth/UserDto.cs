using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertySales.SecureAuth;

public class UserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public string Token { get; set; }
}
