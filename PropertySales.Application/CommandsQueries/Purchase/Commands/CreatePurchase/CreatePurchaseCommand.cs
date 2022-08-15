using MediatR;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.CreatePurchase;

public class CreatePurchaseCommand : IRequest<long>
{
    public long? UserId { get; set; }
    public long? HouseId { get; set; }
}