using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Location.Commands.CreateLocation;
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

    [HttpPost("add-location")]
    public async Task<ActionResult<long>> Create([FromBody] CreateLocationDto dto)
    {
        var createLocationCommand = _mapper.Map<CreateLocationCommand>(dto);
        var locationId = await Mediator.Send(createLocationCommand);

        return Created("api/locations", locationId);
    }
}