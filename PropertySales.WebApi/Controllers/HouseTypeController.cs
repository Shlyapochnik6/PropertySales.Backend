using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.HouseType.Commands.CreateHouseType;
using PropertySales.Application.CommandsQueries.HouseType.Commands.DeleteHouseType;
using PropertySales.Application.CommandsQueries.HouseType.Commands.UpdateHouseType;
using PropertySales.Application.CommandsQueries.HouseType.Queries.GetHouseType;
using PropertySales.Application.CommandsQueries.HouseType.Queries.GetListHouseTypes;
using PropertySales.Application.Interfaces;
using PropertySales.WebApi.Models.HouseType;

namespace PropertySales.WebApi.Controllers;

[Route("api/house-types")]
public class HouseTypeController : BaseController
{
    private readonly IMapper _mapper;
    
    public HouseTypeController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("get-house-type/{id:long}")]
    public async Task<ActionResult<HouseTypeVm>> Get(long id)
    {
        var getHouseTypeQuery = new GetHouseTypeQuery()
        {
            Id = id
        };
        var houseTypeVm = await Mediator.Send(getHouseTypeQuery);

        return Ok(houseTypeVm);
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<HouseTypeDto>>> GetAll()
    {
        var getListHouseTypeQuery = new GetListHouseTypeQuery();
        var listHouseTypeVm = await Mediator.Send(getListHouseTypeQuery);

        return Ok(listHouseTypeVm.HouseTypes);
    }
    
    [HttpPost("add-house-type")]
    public async Task<ActionResult<long>> Create([FromBody] CreateHouseTypeDto dto)
    {
        var createHouseTypeCommand = _mapper.Map<CreateHouseTypeCommand>(dto);
        var houseTypeId = await Mediator.Send(createHouseTypeCommand);

        return Created("api/house-types", houseTypeId);
    }

    [HttpDelete("delete-house-type/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var deleteHouseTypeCommand = new DeleteHouseTypeCommand()
        {
            Id = id
        };
        await Mediator.Send(deleteHouseTypeCommand);

        return NoContent();
    }

    [HttpPut("update-house-type/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateHouseTypeDto dto)
    {
        var updateHouseTypeCommand = _mapper.Map<UpdateHouseTypeCommand>(dto);
        updateHouseTypeCommand.Id = id;

        await Mediator.Send(updateHouseTypeCommand);

        return NoContent();
    }
}