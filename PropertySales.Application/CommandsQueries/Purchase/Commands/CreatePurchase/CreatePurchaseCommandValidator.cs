using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.CreatePurchase;

public class CreatePurchaseCommandValidator : AbstractValidator<CreatePurchaseCommand>
{
    public CreatePurchaseCommandValidator()
    {
        RuleFor(purchase => purchase.UserId).NotEmpty();
        RuleFor(purchase => purchase.HouseId).NotEmpty();
    }
}