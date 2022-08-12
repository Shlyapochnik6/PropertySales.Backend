using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.HouseType.Commands.CreateHouseType;
using PropertySales.Application.CommandsQueries.HouseType.Commands.DeleteHouseType;
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
}