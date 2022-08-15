using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.UpdatePurchase;

public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;

    public UpdatePurchaseCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var house = await _dbContext.Houses
            .FirstOrDefaultAsync(house => house.Id == request.HouseId, cancellationToken);
        if (house == null)
            throw new NotFoundException(nameof(Domain.House), request.HouseId);
        
        var purchase = await _dbContext.Purchases
            .FirstOrDefaultAsync(purchase => purchase.Id == request.PurchaseId &&
                                        purchase.User.Id == request.UserId, cancellationToken);
        if (purchase == null)
            throw new NotFoundException(nameof(Domain.Purchase), request.PurchaseId);

        purchase.House = house;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}