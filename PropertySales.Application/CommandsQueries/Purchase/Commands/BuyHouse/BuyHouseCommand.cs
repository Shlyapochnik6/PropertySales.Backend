using MediatR;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.BuyHouse;

public class BuyHouseCommand : IRequest<long>
{
    public long? UserId { get; set; }
    public long? PurchaseId { get; set; }
    public string PublisherName { get; set; }
    public string HouseName { get; set; }
}