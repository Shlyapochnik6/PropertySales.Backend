using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.DeleteHouseType;

public class DeleteHouseTypeCommandHandler : IRequestHandler<DeleteHouseTypeCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;

    public DeleteHouseTypeCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeleteHouseTypeCommand request, CancellationToken cancellationToken)
    {
        var houseType = await _dbContext.HouseTypes
            .FirstOrDefaultAsync(houseType => houseType.Id == request.Id, cancellationToken);

        if (houseType == null)
            throw new NotFoundException(nameof(Domain.HouseType), request.Id);

        _dbContext.HouseTypes.Remove(houseType);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}