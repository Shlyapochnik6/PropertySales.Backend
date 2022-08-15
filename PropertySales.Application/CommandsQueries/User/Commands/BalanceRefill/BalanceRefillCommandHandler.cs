using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.User.Commands.BalanceRefill;

public class BalanceRefillCommandHandler : IRequestHandler<BalanceRefillCommand, string>
{
    private readonly IPropertySalesDbContext _dbContext;

    public BalanceRefillCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<string> Handle(BalanceRefillCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.UserId);

        user.Balance += request.ReplenishmentAmount;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return $"Congratulations! Balance was refilled! Your balance is {user.Balance}";
    }
}