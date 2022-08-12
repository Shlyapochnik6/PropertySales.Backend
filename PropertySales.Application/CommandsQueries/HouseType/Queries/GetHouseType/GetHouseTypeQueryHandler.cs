using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.HouseType.Queries.GetHouseType;

public class GetHouseTypeQueryHandler : IRequestHandler<GetHouseTypeQuery, HouseTypeVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetHouseTypeQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<HouseTypeVm> Handle(GetHouseTypeQuery request, CancellationToken cancellationToken)
    {
        var houseTypeQuery = await _dbContext.HouseTypes
            .Include(h => h.Houses).ThenInclude(h => h.Publisher)
            .FirstOrDefaultAsync(houseType => houseType.Id == request.Id, cancellationToken);

        return _mapper.Map<HouseTypeVm>(houseTypeQuery);
    }
}