using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Location.Queries.GetLocation;

public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, LocationVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Location> _cacheManager;

    public GetLocationQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper,
        ICacheManager<Domain.Location> cacheManager)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<LocationVm> Handle(GetLocationQuery request, CancellationToken cancellationToken)
    {
        var locationQuery = async () => await _dbContext.Locations
            .Include(h => h.Houses)
            .FirstOrDefaultAsync(location => location.Id == request.Id, cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var location = await _cacheManager.GetOrSetCacheValue(request.Id, locationQuery);
        
        return _mapper.Map<LocationVm>(location);
    }
}