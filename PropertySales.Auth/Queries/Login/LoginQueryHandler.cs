using System.Net;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Domain;
using PropertySales.SecureAuth.Interfaces;

namespace PropertySales.SecureAuth.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticatedResponse>
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

    public async Task<AuthenticatedResponse> Handle(LoginQuery loginQuery, CancellationToken cancellationToken)
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
            var refreshToken = _jwtGenerator.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return new AuthenticatedResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Balance = user.Balance,
                Token = _jwtGenerator.CreateToken(user),
                RefreshToken = user.RefreshToken
            };
        }

        throw new Exception(HttpStatusCode.Unauthorized.ToString());
    }
}
