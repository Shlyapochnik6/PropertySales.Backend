using MediatR;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.UpdatePurchase;

public class UpdatePurchaseCommand : IRequest
{
    public long? UserId { get; set; }
    public long? PurchaseId { get; set; }
    public long? HouseId { get; set; }
}