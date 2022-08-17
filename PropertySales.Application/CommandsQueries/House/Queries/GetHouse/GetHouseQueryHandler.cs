using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.House.Queries.GetHouse;

public class GetHouseQueryHandler : IRequestHandler<GetHouseQuery, HouseVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.House> _cacheManager;

    public GetHouseQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper,
        ICacheManager<Domain.House> cacheManager)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }

    public async Task<HouseVm> Handle(GetHouseQuery request, CancellationToken cancellationToken)
    {
        var houseQuery = async () => await _dbContext.Houses
            .Include(h => h.Publisher)
            .Include(h => h.HouseType)
            .Include(h => h.Location)
            .FirstOrDefaultAsync(house => house.Id == request.Id, cancellationToken);
        
        if (houseQuery == null)
            throw new NotFoundException(nameof(Domain.House), request.Id);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var house = await _cacheManager.GetOrSetCacheValue(request.Id, houseQuery);

        house.HouseType.Houses = null!;
        house.Location.Houses = null!;
        house.Publisher.Houses = null!;

        return _mapper.Map<HouseVm>(house);
    }
}