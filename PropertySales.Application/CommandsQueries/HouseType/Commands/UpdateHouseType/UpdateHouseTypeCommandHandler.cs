using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.UpdateHouseType;

public class UpdateHouseTypeCommandHandler : IRequestHandler<UpdateHouseTypeCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly ICacheManager<Domain.HouseType> _cacheManager;

    public UpdateHouseTypeCommandHandler(IPropertySalesDbContext dbContext,
        ICacheManager<Domain.HouseType> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<Unit> Handle(UpdateHouseTypeCommand request, CancellationToken cancellationToken)
    {
        var wrongInfo = await _dbContext.HouseTypes
            .AnyAsync(houseType => houseType.Name == request.Name && 
                 houseType.Id != request.Id, cancellationToken);

        if (wrongInfo)
            throw new RecordExistsException(request.Name);

        var houseType = await _dbContext.HouseTypes
            .FirstOrDefaultAsync(houseType => houseType.Id == request.Id, cancellationToken);

        if (houseType == null)
            throw new NotFoundException(nameof(Domain.HouseType), request.Id);
        
        houseType.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, houseType);
        
        return Unit.Value;
    }
}