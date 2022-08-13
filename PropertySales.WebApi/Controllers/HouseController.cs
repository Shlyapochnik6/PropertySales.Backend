using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.House.Commands.CreateHouse;

namespace PropertySales.WebApi.Controllers;

[Microsoft.AspNetCore.Components.Route("api/houses")]
public class HouseController : BaseController
{
    private readonly IMapper _mapper;

    public HouseController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost("add-house")]
    public async Task<ActionResult<long>> Create([FromBody] CreateHouseCommand house)
    {
        var houseId = await Mediator.Send(house);

        return Created("api/houses", houseId);
    }
}