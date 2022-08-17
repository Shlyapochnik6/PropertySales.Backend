using MediatR;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.DeletePurchase;

public class DeletePurchaseCommand : IRequest
{
    public long? PurchaseId { get; set; }
    public long? UserId { get; set; }
}