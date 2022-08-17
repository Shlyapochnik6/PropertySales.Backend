using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.SecureAuth.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly UserManager<Domain.User> _userManager;

    public UpdateUserCommandHandler(UserManager<Domain.User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.Id);

        if (!await _userManager.CheckPasswordAsync(user, request.OldPassword))
            throw new Exception("Invalid password");
        
        user.UserName = request.UserName;
        user.Email = request.Email;
        
        await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        await _userManager.UpdateAsync(user);
        
        return Unit.Value;
    }
}