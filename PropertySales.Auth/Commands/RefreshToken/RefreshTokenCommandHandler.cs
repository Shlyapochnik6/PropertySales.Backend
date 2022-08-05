using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertySales.Application.Common.Exceptions;
using PropertySales.SecureAuth.Interfaces;
using System.Security.Claims;

namespace PropertySales.SecureAuth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticatedResponse>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<Domain.User> _userManager;

    public RefreshTokenCommandHandler(IJwtGenerator jwtGenerator,
        UserManager<Domain.User> userManager)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
    }

    public async Task<AuthenticatedResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = _jwtGenerator.GetPrincipalFromExpiredToken(request.Token);

        var userId = Convert.ToInt64(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            throw new NotFoundException(nameof(Domain.User), null!));

        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), userId);

        if (user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new Exception("Invalid client request");

        var newToken = _jwtGenerator.CreateToken(user);
        var newRefreshToken = _jwtGenerator.CreateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        return new AuthenticatedResponse()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Balance = user.Balance,
            Token = newToken,
            RefreshToken = newRefreshToken
        };
    }
}
