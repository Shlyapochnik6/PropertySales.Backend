namespace PropertySales.Application.CommandsQueries.Purchase.Queries.GetListPurchases;

public class GetPurchaseListVm
{
    public IEnumerable<PurchaseDto> Purchases { get; set; }
}