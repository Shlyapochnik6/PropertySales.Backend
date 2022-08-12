using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Publisher.Commands.CreatePublisher;
using PropertySales.Application.CommandsQueries.Publisher.Commands.DeletePublisher;
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
        var createPublisherCommand = _mapper.Map<CreatePublisherCommand>(dto);
        var publisherId = await Mediator.Send(createPublisherCommand);

        return Created("api/publisher", publisherId);
    }

    [HttpDelete("delete-publisher/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var deletePublisherCommand = new DeletePublisherCommand
        {
            Id = id
        };
        await Mediator.Send(deletePublisherCommand);

        return NoContent();
    }
}