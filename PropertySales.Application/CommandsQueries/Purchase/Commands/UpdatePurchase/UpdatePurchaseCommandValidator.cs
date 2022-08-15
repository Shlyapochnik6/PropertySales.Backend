using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.UpdatePurchase;

public class UpdatePurchaseCommandValidator : AbstractValidator<UpdatePurchaseCommand>
{
    public UpdatePurchaseCommandValidator()
    {
        RuleFor(purchase => purchase.UserId).NotEmpty();
        RuleFor(purchase => purchase.HouseId).NotEmpty();
        RuleFor(purchase => purchase.PurchaseId).NotEmpty();
    }
}