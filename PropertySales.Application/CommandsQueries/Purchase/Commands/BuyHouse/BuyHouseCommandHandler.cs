using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Purchase.Commands.BuyHouse;

public class BuyHouseCommandHandler : IRequestHandler<BuyHouseCommand, long>
{
    private readonly IPropertySalesDbContext _dbContext;

    public BuyHouseCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<long> Handle(BuyHouseCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
        if (user == null) 
            throw new NotFoundException(nameof(Domain.User), request.UserId);
        
        var purchase = await _dbContext.Purchases
            .Include(purchase => purchase.House)
            .FirstOrDefaultAsync(purchase => purchase.Id == request.PurchaseId &&
                                             purchase.User.Id == request.UserId, cancellationToken);
        if (purchase == null)
            throw new NotFoundException(nameof(Domain.Purchase), request.PurchaseId);
        
        var publisher = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Name == request.PublisherName, cancellationToken);
        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.PublisherName);
        
        var house = await _dbContext.Houses
            .FirstOrDefaultAsync(house => house.Name == request.HouseName, cancellationToken);
        if (house == null)
            throw new NotFoundException(nameof(Domain.House), request.HouseName);
        
        if (user.Balance < purchase.House.Price)
            throw new Exception($"Not sufficient funds, balance = {user.Balance}");

        user.Balance -= purchase.House.Price;
        publisher.Houses.Remove(house);
        purchase.BuyTime = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return purchase.Id;
    }
}