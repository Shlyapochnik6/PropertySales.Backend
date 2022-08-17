using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Location.Queries.GetListLocation;

public class GetListLocationQueryHandler : IRequestHandler<GetListLocationQuery, GetListLocationVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListLocationQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetListLocationVm> Handle(GetListLocationQuery request, CancellationToken cancellationToken)
    {
        var locations = await _dbContext.Locations
            .ProjectTo<LocationDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        
        return new GetListLocationVm() { Locations = locations };
    }
}