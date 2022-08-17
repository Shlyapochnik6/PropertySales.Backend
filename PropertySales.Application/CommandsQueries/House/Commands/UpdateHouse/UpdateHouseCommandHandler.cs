using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.House.Commands.UpdateHouse;

public class UpdateHouseCommandHandler : IRequestHandler<UpdateHouseCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly ICacheManager<Domain.House> _cacheManager;
    
    public UpdateHouseCommandHandler(IPropertySalesDbContext dbContext,
        ICacheManager<Domain.House> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<Unit> Handle(UpdateHouseCommand request, CancellationToken cancellationToken)
    {
        var house = await _dbContext.Houses
            .FirstOrDefaultAsync(house => house.Id == request.Id, cancellationToken);
        
        var publisher = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == request.PublisherId, cancellationToken);
        
        var houseType = await _dbContext.HouseTypes
            .FirstOrDefaultAsync(houseType => houseType.Id == request.HouseTypeId, cancellationToken);
        
        var location = await _dbContext.Locations
            .FirstOrDefaultAsync(location => location.Id == request.LocationId, cancellationToken);
        
        var wrongInfo = await _dbContext.Houses
            .AnyAsync(house => house.Name == request.Name &&
                               house.Id != request.Id, cancellationToken);

        if (house == null)
            throw new NotFoundException(nameof(Domain.House), request.Id);
        
        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.PublisherId);

        if (houseType == null)
            throw new NotFoundException(nameof(Domain.HouseType), request.HouseTypeId);
        
        if (location == null)
            throw new NotFoundException(nameof(Domain.Location), request.LocationId);

        if (wrongInfo)
            throw new RecordExistsException(request.Name);

        house.Name = request.Name;
        house.Description = request.Description;
        house.Material = request.Material;
        house.Price = request.Price;
        house.FloorArea = request.FloorArea;
        house.YearBuilt = request.YearBuilt;
        house.HouseType = houseType;
        house.Publisher = publisher;
        house.Location = location;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, house);
        
        return Unit.Value;
    }
}