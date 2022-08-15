using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.DeletePurchase;

public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;

    public DeletePurchaseCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _dbContext.Purchases
            .FirstOrDefaultAsync(purchase => purchase.User.Id == request.UserId &&
                                             purchase.Id == request.PurchaseId, cancellationToken);
        if (purchase == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.PurchaseId);

        _dbContext.Purchases.Remove(purchase);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}