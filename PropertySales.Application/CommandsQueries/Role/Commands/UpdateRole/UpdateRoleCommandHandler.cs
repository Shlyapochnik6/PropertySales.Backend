using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertySales.Application.Common.Exceptions;

namespace PropertySales.Application.CommandsQueries.Role.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public UpdateRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }
    
    public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null)
            throw new NotFoundException(nameof(IdentityRole<long>), request.RoleId);

        var roleExists = await _roleManager.FindByNameAsync(request.Name.ToString()) != null;
        if (roleExists)
            throw new RecordExistsException(request.Name);
        
        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpper();
        
        await _roleManager.UpdateAsync(role);
        
        return Unit.Value;
    }
}