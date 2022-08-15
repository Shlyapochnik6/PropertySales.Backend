using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.CreatePurchase;

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, long>
{
    private readonly IPropertySalesDbContext _dbContext;

    public CreatePurchaseCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<long> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.UserId);
        
        var house = await _dbContext.Houses
            .FirstOrDefaultAsync(house => house.Id == request.HouseId, cancellationToken);
        if (house == null)
            throw new NotFoundException(nameof(Domain.House), request.HouseId);

        var purchaseCopy = await _dbContext.Purchases
            .Include(u => u.User)
            .Include(h => h.House)
            .AnyAsync(purchase => purchase.User.Id == request.UserId && 
                                  purchase.House.Id == house.Id, cancellationToken);
        if (purchaseCopy)
            throw new RecordExistsException("Purchase");

        var purchase = new Domain.Purchase()
        {
            User = user,
            House = house
        };

        await _dbContext.Purchases.AddAsync(purchase, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return purchase.Id;
    }
}