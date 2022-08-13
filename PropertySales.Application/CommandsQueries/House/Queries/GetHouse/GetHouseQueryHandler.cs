using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.House.Queries.GetHouse;

public class GetHouseQueryHandler : IRequestHandler<GetHouseQuery, HouseVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetHouseQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<HouseVm> Handle(GetHouseQuery request, CancellationToken cancellationToken)
    {
        var houseQuery = await _dbContext.Houses
            .Include(h => h.Publisher)
            .Include(h => h.HouseType)
            .Include(h => h.Location)
            .FirstOrDefaultAsync(house => house.Id == request.Id, cancellationToken);

        if (houseQuery == null)
            throw new NotFoundException(nameof(Domain.House), request.Id);

        houseQuery.HouseType.Houses = null!;
        houseQuery.Location.Houses = null!;
        houseQuery.Publisher.Houses = null!;

        return _mapper.Map<HouseVm>(houseQuery);
    }
}