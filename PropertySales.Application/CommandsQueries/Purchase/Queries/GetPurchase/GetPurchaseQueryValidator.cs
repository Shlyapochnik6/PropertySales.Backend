using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Purchase.Queries.GetPurchase;

public class GetPurchaseQueryValidator : AbstractValidator<GetPurchaseQuery>
{
    public GetPurchaseQueryValidator()
    {
        RuleFor(purchase => purchase.Id).NotEmpty();
    }
}