using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.CommandsQueries.Publisher.Queries.GetPublisher;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Purchase.Queries.GetPurchase;

public class GetPurchaseQueryHandler : IRequestHandler<GetPurchaseQuery, PurchaseVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Purchase> _cacheManager;

    public GetPurchaseQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper,
        ICacheManager<Domain.Purchase> cacheManager)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<PurchaseVm> Handle(GetPurchaseQuery request, CancellationToken cancellationToken)
    {
        var purchaseQuery = async () => await _dbContext.Purchases
            .Include(purchase => purchase.User)
            .Include(purchase => purchase.House)
                .ThenInclude(purchase => purchase.Publisher)
            .Include(purchase => purchase.House)
                .ThenInclude(purchase => purchase.HouseType)
            .Include(purchase => purchase.House)
                .ThenInclude(purchase => purchase.Location)
            .FirstOrDefaultAsync(purchase => purchase.Id == request.Id &&
                                             purchase.User.Id == request.UserId, cancellationToken);
        
        if (purchaseQuery == null) 
            throw new NotFoundException(nameof(Domain.Purchase), request.Id);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var purchase = await _cacheManager.GetOrSetCacheValue(request.Id, purchaseQuery);
        
        purchase.House.Purchases = null!;

        return _mapper.Map<PurchaseVm>(purchase);
    }
}