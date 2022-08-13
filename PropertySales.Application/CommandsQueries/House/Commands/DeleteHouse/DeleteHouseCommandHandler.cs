using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.House.Commands.DeleteHouse;

public class DeleteHouseCommandHandler : IRequestHandler<DeleteHouseCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;

    public DeleteHouseCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeleteHouseCommand request, CancellationToken cancellationToken)
    {
        var house = await _dbContext.Houses
            .FirstOrDefaultAsync(house => house.Id == request.Id, cancellationToken);
        
        if (house == null)
            throw new NotFoundException(nameof(Domain.House), request.Id);
        
        _dbContext.Houses.Remove(house);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}