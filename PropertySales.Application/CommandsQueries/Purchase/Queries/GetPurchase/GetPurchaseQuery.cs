using MediatR;

namespace PropertySales.Application.CommandsQueries.Purchase.Queries.GetPurchase;

public class GetPurchaseQuery : IRequest<PurchaseVm>
{
    public long? Id { get; set; }
    public long? UserId { get; set; }
}