using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PropertySales.Application.CommandsQueries.Role.Queries.GetListRoles;

public class GetListRoleQueryHandler : IRequestHandler<GetListRoleQuery, GetListRoleVm>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly IMapper _mapper;

    public GetListRoleQueryHandler(RoleManager<IdentityRole<long>> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }
    
    public async Task<GetListRoleVm> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles
            .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return new GetListRoleVm() { Roles = roles };
    }
}