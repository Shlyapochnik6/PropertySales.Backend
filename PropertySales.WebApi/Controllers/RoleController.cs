using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Role.Commands.CreateRole;

namespace PropertySales.WebApi.Controllers;

[Route("api/roles")]
public class RoleController : BaseController
{
    private readonly IMapper _mapper;

    public RoleController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost("add-role")]
    public async Task<ActionResult<long>> Create([FromBody] CreateRoleCommand role)
    {
        var roleId = await Mediator.Send(role);
        
        return Created("api/roles", roleId);
    }
}