using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Domain;
using PropertySales.SecureAuth.Interfaces;

namespace PropertySales.SecureAuth.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<UserDto> Handle(LoginQuery loginQuery, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(loginQuery.Email);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), loginQuery.Email);
        }
        
        var result = await _signInManager
            .CheckPasswordSignInAsync(user, loginQuery.Password, false);

        if (result.Succeeded)
        {
            return new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _jwtGenerator.CreateToken(user),
                Balance = 0
            };
        }

        throw new Exception(HttpStatusCode.Unauthorized.ToString());
    }
}
