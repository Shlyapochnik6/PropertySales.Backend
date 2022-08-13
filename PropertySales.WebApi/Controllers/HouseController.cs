using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.House.Commands.CreateHouse;
using PropertySales.Application.CommandsQueries.House.Commands.DeleteHouse;
using PropertySales.Application.CommandsQueries.House.Commands.UpdateHouse;
using PropertySales.Application.CommandsQueries.House.Queries.GetHouse;
using PropertySales.Application.CommandsQueries.House.Queries.GetListHouses;
using PropertySales.WebApi.Models.House;

namespace PropertySales.WebApi.Controllers;

[Microsoft.AspNetCore.Components.Route("api/houses")]
public class HouseController : BaseController
{
    private readonly IMapper _mapper;

    public HouseController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("get-house/{id:long}")]
    public async Task<ActionResult<HouseVm>> Get(long id)
    {
        var getHouseQuery = new GetHouseQuery()
        {
            Id = id
        };
        var houseVm = await Mediator.Send(getHouseQuery);

        return Ok(houseVm);
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<HouseDto>>> GetAll()
    {
        var getListHouseQuery = new GetListHouseQuery();
        var listHouseVm = await Mediator.Send(getListHouseQuery);

        return Ok(listHouseVm.Houses);
    }
    
    [HttpPost("add-house")]
    public async Task<ActionResult<long>> Create([FromBody] CreateHouseCommand house)
    {
        var houseId = await Mediator.Send(house);

        return Created("api/houses", houseId);
    }

    [HttpDelete("delete-house/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var deleteHouseCommand = new DeleteHouseCommand()
        {
            Id = id
        };
        await Mediator.Send(deleteHouseCommand);

        return NoContent();
    }

    [HttpPut("update-house/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateHouseDto dto)
    {
        var updateHouseCommand = _mapper.Map<UpdateHouseCommand>(dto);
        updateHouseCommand.Id = id;

        await Mediator.Send(updateHouseCommand);

        return NoContent();
    }
}