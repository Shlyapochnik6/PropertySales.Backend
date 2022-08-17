using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.House.Queries.GetListHouses;

public class GetListHouseQueryHandler : IRequestHandler<GetListHouseQuery, GetListHouseVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListHouseQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetListHouseVm> Handle(GetListHouseQuery request, CancellationToken cancellationToken)
    {
        var houses = await _dbContext.Houses
            .ProjectTo<HouseDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new GetListHouseVm() { Houses = houses };
    }
}