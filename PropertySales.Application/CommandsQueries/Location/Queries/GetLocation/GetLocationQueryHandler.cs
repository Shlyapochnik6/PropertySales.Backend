using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Location.Queries.GetLocation;

public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, LocationVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetLocationQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<LocationVm> Handle(GetLocationQuery request, CancellationToken cancellationToken)
    {
        var locationQuery = await _dbContext.Locations
            .Include(h => h.Houses)
            .FirstOrDefaultAsync(location => location.Id == request.Id, cancellationToken);

        return _mapper.Map<LocationVm>(locationQuery);
    }
}