using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Publisher.Commands.CreatePublisher;
using PropertySales.WebApi.Models.Publisher;

namespace PropertySales.WebApi.Controllers;

[Route("api/publisher")]
public class PublisherController : BaseController
{
    private readonly IMapper _mapper;

    public PublisherController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost("add-publisher")]
    public async Task<ActionResult<long>> Create([FromBody] CreatePublisherDto dto)
    {
        var command = _mapper.Map<CreatePublisherCommand>(dto);
        var publisherId = await Mediator.Send(command);

        return Created("api/publisher", publisherId);
    }
}