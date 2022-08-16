using MediatR;
using Microsoft.AspNetCore.Identity;
using PropertySales.Application.Common.Exceptions;

namespace PropertySales.Application.CommandsQueries.Role.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, long>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public CreateRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }
    
    public async Task<long> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole<long>(request.Name));

        if (!result.Succeeded)
            throw new Exception("Role creation error");
        
        var role = await _roleManager.FindByNameAsync(request.Name);
        
        return role.Id;
    }
}