using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Role.Commands.CreateRole;
using PropertySales.Application.CommandsQueries.Role.Commands.DeleteRole;
using PropertySales.Application.CommandsQueries.Role.Commands.SetRole;
using PropertySales.Application.CommandsQueries.Role.Commands.UpdateRole;
using PropertySales.Application.CommandsQueries.Role.Queries.GetListRoles;
using PropertySales.Application.CommandsQueries.Role.Queries.GetRole;
using PropertySales.WebApi.Models.Role;

namespace PropertySales.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/roles")]
public class RoleController : BaseController
{
    private readonly IMapper _mapper;

    public RoleController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("get-role/{id:long}")]
    public async Task<ActionResult> Get(long id)
    {
        var getRoleQuery = new GetRoleQuery()
        {
            RoleId = id
        };
        var roleVm = await Mediator.Send(getRoleQuery);

        return Ok(roleVm);
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll()
    {
        var getListRoleQuery = new GetListRoleQuery();
        var listRoleVm = await Mediator.Send(getListRoleQuery);

        return Ok(listRoleVm.Roles);
    }

    [HttpPost("add-role")]
    public async Task<ActionResult<long>> Create([FromBody] CreateRoleCommand role)
    {
        var roleId = await Mediator.Send(role);
        
        return Created("api/roles", roleId);
    }

    [HttpDelete("delete-role/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var deleteRoleCommand = new DeleteRoleCommand()
        {
            RoleId = id
        };
        await Mediator.Send(deleteRoleCommand);

        return NoContent();
    }

    [HttpPut("update-role/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateRoleDto dto)
    {
        var updateRoleCommand = _mapper.Map<UpdateRoleCommand>(dto);
        updateRoleCommand.RoleId = id;

        await Mediator.Send(updateRoleCommand);

        return NoContent();
    }

    [HttpPost("set-role")]
    public async Task<ActionResult> SetRole([FromBody] SetRoleDto dto)
    {
        var setRoleCommand = _mapper.Map<SetRoleCommand>(dto);
        await Mediator.Send(setRoleCommand);

        return NoContent();
    }
}