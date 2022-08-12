using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.HouseType.Queries.GetListHouseTypes;

public class GetListHouseTypeQueryHandler : IRequestHandler<GetListHouseTypeQuery, GetListHouseTypeVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListHouseTypeQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetListHouseTypeVm> Handle(GetListHouseTypeQuery request, CancellationToken cancellationToken)
    {
        var houseTypes = await _dbContext.HouseTypes
            .ProjectTo<HouseTypeDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new GetListHouseTypeVm() { HouseTypes = houseTypes };
    }
}