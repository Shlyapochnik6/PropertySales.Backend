using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Purchase.Commands.CreatePurchase;
using PropertySales.Application.CommandsQueries.Purchase.Commands.DeletePurchase;
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

    [HttpDelete("delete-purchase/{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var deletePurchaseCommand = new DeletePurchaseCommand()
        {
            PurchaseId = id,
            UserId = UserId
        };
        await Mediator.Send(deletePurchaseCommand);
        
        return NoContent();
    }
}