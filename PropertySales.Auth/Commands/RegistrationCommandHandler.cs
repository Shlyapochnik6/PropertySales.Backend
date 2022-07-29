using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;
using PropertySales.Domain;
using PropertySales.SecureAuth.Exceptions;
using PropertySales.SecureAuth.Interfaces;

namespace PropertySales.SecureAuth.Commands;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, UserDto>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IPropertySalesDbContext _context;
    private readonly IJwtGenerator _jwtGenerator;

    public RegistrationCommandHandler(UserManager<Domain.User> userManager,
       IJwtGenerator jwtGenerator, IPropertySalesDbContext context,
       SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
        _context = context;
        _signInManager = signInManager;
    }

    public async Task<UserDto> Handle(RegistrationCommand registration,
        CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Email == registration.Email, cancellationToken))
        {
            throw new CreateUserException("Email has already been created!");
        }

        var user = new User()
        {
            UserName = registration.Name,
            Email = registration.Email
        };

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            throw new CreateUserException(result.Errors.ToList());
        }

        return new UserDto()
        {
            UserName = user.UserName,
            Email = user.Email,
            Balance = user.Balance,
            Token = _jwtGenerator.CreateToken(user)
        };
    }
}