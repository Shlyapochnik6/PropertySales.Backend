using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Location.Commands.CreateLocation;
using PropertySales.Application.CommandsQueries.Location.Commands.DeleteLocation;
using PropertySales.Application.CommandsQueries.Location.Commands.UpdateLocation;
using PropertySales.Application.CommandsQueries.Location.Queries.GetListLocation;
using PropertySales.Application.CommandsQueries.Location.Queries.GetLocation;
using PropertySales.WebApi.Models.Location;

namespace PropertySales.WebApi.Controllers;

[Route("api/locations")]
public class LocationController : BaseController
{
    private readonly IMapper _mapper;

    public LocationController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("get-location/{id:long}")]
    public async Task<ActionResult<LocationVm>> Get(long id)
    {
        var getLocationQuery = new GetLocationQuery()
        {
            Id = id
        };
        var locationVm = await Mediator.Send(getLocationQuery);

        return Ok(locationVm);
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<LocationDto>>> GetAll()
    {
        var getListLocationQuery = new GetListLocationQuery();
        var listLocationVm = await Mediator.Send(getListLocationQuery);

        return Ok(listLocationVm.Locations);
    }
    
    [HttpPost("add-location")]
    public async Task<ActionResult<long>> Create([FromBody] CreateLocationDto dto)
    {
        var createLocationCommand = _mapper.Map<CreateLocationCommand>(dto);
        var locationId = await Mediator.Send(createLocationCommand);

        return Created("api/locations", locationId);
    }

    [HttpDelete("delete-location/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var deleteLocationCommand = new DeleteLocationCommand()
        {
            Id = id
        };
        await Mediator.Send(deleteLocationCommand);

        return NoContent();
    }

    [HttpPut("update-location/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateLocationDto dto)
    {
        var updateLocationCommand = _mapper.Map<UpdateLocationCommand>(dto);
        updateLocationCommand.Id = id;

        await Mediator.Send(updateLocationCommand);

        return NoContent();
    }
}