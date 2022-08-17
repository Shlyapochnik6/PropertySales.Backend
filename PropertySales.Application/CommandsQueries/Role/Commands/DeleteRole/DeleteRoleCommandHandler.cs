using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertySales.Application.Common.Exceptions;

namespace PropertySales.Application.CommandsQueries.Role.Commands.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }
    
    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        
        if (role == null)
            throw new NotFoundException(nameof(IdentityRole<long>), request.RoleId);

        var result = await _roleManager.DeleteAsync(role);
        
        if (!result.Succeeded)
            throw new Exception("Role deletion error");
        
        return Unit.Value;
    }
}