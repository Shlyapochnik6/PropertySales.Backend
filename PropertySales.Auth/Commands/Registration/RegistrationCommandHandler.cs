using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;
using PropertySales.Domain;
using PropertySales.SecureAuth.Exceptions;
using PropertySales.SecureAuth.Interfaces;

namespace PropertySales.SecureAuth.Commands.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthenticatedResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IPropertySalesDbContext _context;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public RegistrationCommandHandler(UserManager<User> userManager,
       IJwtGenerator jwtGenerator, IPropertySalesDbContext context,
       SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
        _context = context;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<AuthenticatedResponse> Handle(RegistrationCommand registration,
        CancellationToken cancellationToken)
    {
        if (await _context.Users.
            AnyAsync(user => user.Email == registration.Email, cancellationToken))
        {
            throw new CreateUserException("This email has already been created!");
        }

        if (await _context.Users.
            AnyAsync(user => user.UserName == registration.Name, cancellationToken))
        {
            throw new CreateUserException("This user's name already exists");
        }

        var refreshToken = _jwtGenerator.CreateRefreshToken();

        var user = new User()
        {
            UserName = registration.Name,
            Email = registration.Email,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
        };

        var result = await _userManager.CreateAsync(user, registration.Password);

        if (result.Succeeded)
        {
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

        throw new CreateUserException(result.Errors.ToList());
    }
}