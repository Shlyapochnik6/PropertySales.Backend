using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Purchase.Commands.CreatePurchase;
using PropertySales.WebApi.Models.Purchase;

namespace PropertySales.WebApi.Controllers;

[Authorize]
[Route("api/purchases")]
public class PurchaseController : BaseController
{
    private readonly IMapper _mapper;
    
    public PurchaseController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpPost("add-purchase")]
    public async Task<ActionResult<long>> Create([FromBody] CreatePurchaseDto dto)
    {
        var createPurchaseCommand = _mapper.Map<CreatePurchaseCommand>(dto);
        createPurchaseCommand.UserId = UserId;

        var purchaseId = await Mediator.Send(createPurchaseCommand);

        return Created("api/purchases", purchaseId);
    }
}