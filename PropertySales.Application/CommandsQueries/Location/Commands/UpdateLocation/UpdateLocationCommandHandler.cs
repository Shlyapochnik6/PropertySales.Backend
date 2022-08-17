using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Location.Commands.UpdateLocation;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly ICacheManager<Domain.Location> _cacheManager;
    
    public UpdateLocationCommandHandler(IPropertySalesDbContext dbContext,
        ICacheManager<Domain.Location> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var countryCopy = await _dbContext.Locations
            .AnyAsync(location => location.Country == request.Country && 
                 location.Id != request.Id, cancellationToken);
        
        var cityCopy = await _dbContext.Locations
            .AnyAsync(location => location.City == request.City && 
                 location.Id != request.Id, cancellationToken);
        
        var streetCopy = await _dbContext.Locations
            .AnyAsync(location => location.Street == request.Street && 
                 location.Id != request.Id, cancellationToken);
        
        var location = await _dbContext.Locations
            .FirstOrDefaultAsync(location => location.Id == request.Id, cancellationToken);

        if (countryCopy && cityCopy && streetCopy)
            throw new RecordExistsException("Location");
        
        if (location == null)
            throw new NotFoundException(nameof(Domain.Location), request.Id);

        location.Country = request.Country;
        location.City = request.City;
        location.Street = request.Street;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, location);
        
        return Unit.Value;
    }
}