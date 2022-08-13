using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Location.Commands.DeleteLocation;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;

    public DeleteLocationCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _dbContext.Locations
            .FirstOrDefaultAsync(location => location.Id == request.Id, cancellationToken);

        if (location == null)
            throw new NotFoundException(nameof(Domain.Location), request.Id);
        
        _dbContext.Locations.Remove(location);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}