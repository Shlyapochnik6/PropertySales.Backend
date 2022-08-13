using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.House.Commands.CreateHouse;

public class CreateHouseCommandHandler : IRequestHandler<CreateHouseCommand, long>
{
    private readonly IPropertySalesDbContext _dbContext;

    public CreateHouseCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<long> Handle(CreateHouseCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == request.PublisherId, cancellationToken);
        
        var houseType = await _dbContext.HouseTypes
            .FirstOrDefaultAsync(houseType => houseType.Id == request.HouseTypeId, cancellationToken);
        
        var location = await _dbContext.Locations
            .FirstOrDefaultAsync(location => location.Id == request.LocationId, cancellationToken);
        
        var nameCopy = await _dbContext.Houses
            .AnyAsync(house => house.Name == request.Name, cancellationToken);

        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.PublisherId);

        if (houseType == null)
            throw new NotFoundException(nameof(Domain.HouseType), request.HouseTypeId);
        
        if (location == null)
            throw new NotFoundException(nameof(Domain.Location), request.LocationId);

        if (nameCopy)
            throw new RecordExistsException(request.Name);

        var house = new Domain.House()
        {
            Name = request.Name,
            Description = request.Description,
            Material = request.Material,
            Price = request.Price,
            FloorArea = request.FloorArea,
            YearBuilt = request.YearBuilt,
            HouseType = houseType,
            Publisher = publisher,
            Location = location
        };
        await _dbContext.Houses.AddAsync(house, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return house.Id;
    }
}