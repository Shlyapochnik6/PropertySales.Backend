﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Publisher.Commands.CreatePublisher;
using PropertySales.Application.CommandsQueries.Publisher.Commands.DeletePublisher;
using PropertySales.Application.CommandsQueries.Publisher.Commands.UpdatePublisher;
using PropertySales.Application.CommandsQueries.Publisher.Queries.GetListPublishers;
using PropertySales.Application.CommandsQueries.Publisher.Queries.GetPublisher;
using PropertySales.WebApi.Models.Publisher;

namespace PropertySales.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/publishers")]
public class PublisherController : BaseController
{
    private readonly IMapper _mapper;

    public PublisherController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [ResponseCache(CacheProfileName = "Caching")]
    [HttpGet("get-publisher/{id:long}")]
    public async Task<ActionResult<PublisherVm>> Get(long id)
    {
        var getPublisherQuery = new GetPublisherQuery()
        {
            Id = id
        };
        var publisherVm = await Mediator.Send(getPublisherQuery);

        return Ok(publisherVm);
    }
    
    [AllowAnonymous]
    [ResponseCache(CacheProfileName = "Caching")]
    [HttpGet("get-all")]
    public async Task<ActionResult<GetListPublisherVm>> GetAll()
    {
        var getListPublisherQuery = new GetListPublisherQuery();
        var listPublisherVm = await Mediator.Send(getListPublisherQuery);

        return Ok(listPublisherVm.Publishers);
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

    [HttpPut("update-publisher/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdatePublisherDto dto)
    {
        var updatePublisherCommand = _mapper.Map<UpdatePublisherCommand>(dto);
        updatePublisherCommand.Id = id;
        
        await Mediator.Send(updatePublisherCommand);
        
        return NoContent();
    }
}