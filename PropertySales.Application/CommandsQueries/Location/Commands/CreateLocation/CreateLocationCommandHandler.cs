using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Location.Commands.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, long>
{
    private readonly IPropertySalesDbContext _dbContext;

    public CreateLocationCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<long> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var countryCopy = await _dbContext.Locations
            .AnyAsync(location => location.Country == request.Country, cancellationToken);
        
        var cityCopy = await _dbContext.Locations
            .AnyAsync(location => location.City == request.City, cancellationToken);
        
        var streetCopy = await _dbContext.Locations
            .AnyAsync(location => location.Street == request.Street, cancellationToken);
        
        if (countryCopy && cityCopy && streetCopy)
            throw new RecordExistsException("Location");

        var location = new Domain.Location()
        {
            Country = request.Country,
            City = request.City,
            Street = request.Street
        };

        await _dbContext.Locations.AddAsync(location);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return location.Id;
    }
}