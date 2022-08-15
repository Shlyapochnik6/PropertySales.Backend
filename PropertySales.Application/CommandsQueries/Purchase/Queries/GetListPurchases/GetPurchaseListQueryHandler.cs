using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Purchase.Queries.GetListPurchases;

public class GetPurchaseListQueryHandler : IRequestHandler<GetPurchaseListQuery, GetPurchaseListVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPurchaseListQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetPurchaseListVm> Handle(GetPurchaseListQuery request, CancellationToken cancellationToken)
    {
        var purchases = await _dbContext.Purchases
            .Include(purchase => purchase.User)
            .Include(purchase => purchase.House)
                .ThenInclude(purchase => purchase.Publisher)
            .Include(purchase => purchase.House)
                .ThenInclude(purchase => purchase.HouseType)
            .Include(purchase => purchase.House)
                .ThenInclude(purchase => purchase.Location)
            .Where(purchase => purchase.User.Id == request.UserId)
            .ProjectTo<PurchaseDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetPurchaseListVm() { Purchases = purchases };
    }
}