using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.HouseType.Queries.GetHouseType;

public class GetHouseTypeQueryHandler : IRequestHandler<GetHouseTypeQuery, HouseTypeVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.HouseType> _cacheManager;

    public GetHouseTypeQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper,
        ICacheManager<Domain.HouseType> cacheManager)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<HouseTypeVm> Handle(GetHouseTypeQuery request, CancellationToken cancellationToken)
    {
        var houseTypeQuery = async () => await _dbContext.HouseTypes
            .Include(h => h.Houses).ThenInclude(h => h.Publisher)
            .FirstOrDefaultAsync(houseType => houseType.Id == request.Id, cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var houseType = await _cacheManager.GetOrSetCacheValue(request.Id, houseTypeQuery);
        
        return _mapper.Map<HouseTypeVm>(houseType);
    }
}