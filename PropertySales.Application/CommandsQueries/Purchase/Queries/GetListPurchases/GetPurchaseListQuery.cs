using MediatR;

namespace PropertySales.Application.CommandsQueries.Purchase.Queries.GetListPurchases;

public class GetPurchaseListQuery : IRequest<GetPurchaseListVm>
{
    public long? UserId { get; set; }
}