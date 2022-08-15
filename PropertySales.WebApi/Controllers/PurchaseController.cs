using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertySales.Application.CommandsQueries.Purchase.Commands.BuyHouse;
using PropertySales.Application.CommandsQueries.Purchase.Commands.CreatePurchase;
using PropertySales.Application.CommandsQueries.Purchase.Commands.DeletePurchase;
using PropertySales.Application.CommandsQueries.Purchase.Commands.UpdatePurchase;
using PropertySales.Application.CommandsQueries.Purchase.Queries.GetPurchase;
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

    [HttpGet("get-purchase/{id:long}")]
    public async Task<ActionResult<PurchaseVm>> Get(long id)
    {
        var getPurchaseQuery = new GetPurchaseQuery()
        {
            Id = id,
            UserId = UserId
        };
        var puchaseVm = await Mediator.Send(getPurchaseQuery);

        return Ok(puchaseVm);
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

    [HttpPut("update-purchase/{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdatePurchaseDto dto)
    {
        var updatePurchaseCommand = _mapper.Map<UpdatePurchaseCommand>(dto);
        updatePurchaseCommand.PurchaseId = id;
        updatePurchaseCommand.UserId = UserId;

        await Mediator.Send(updatePurchaseCommand);
        
        return NoContent();
    }

    [HttpPost("buy-house/{id:long}")]
    public async Task<ActionResult> Buy(long id, [FromBody] BuyHouseDto dto)
    {
        var buyHouseCommand = _mapper.Map<BuyHouseCommand>(dto);
        buyHouseCommand.PurchaseId = id;
        buyHouseCommand.UserId = UserId;

        var purchaseId = Mediator.Send(buyHouseCommand);

        return Ok(purchaseId);
    }
}