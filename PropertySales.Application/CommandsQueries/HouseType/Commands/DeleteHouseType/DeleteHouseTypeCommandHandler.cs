using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.DeleteHouseType;

public class DeleteHouseTypeCommandHandler : IRequestHandler<DeleteHouseTypeCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly ICacheManager<Domain.HouseType> _cacheManager;

    public DeleteHouseTypeCommandHandler(IPropertySalesDbContext dbContext,
        ICacheManager<Domain.HouseType> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<Unit> Handle(DeleteHouseTypeCommand request, CancellationToken cancellationToken)
    {
        var houseType = await _dbContext.HouseTypes
            .FirstOrDefaultAsync(houseType => houseType.Id == request.Id, cancellationToken);

        if (houseType == null)
            throw new NotFoundException(nameof(Domain.HouseType), request.Id);

        _dbContext.HouseTypes.Remove(houseType);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.RemoveCacheValue(request.Id);
        
        return Unit.Value;
    }
}