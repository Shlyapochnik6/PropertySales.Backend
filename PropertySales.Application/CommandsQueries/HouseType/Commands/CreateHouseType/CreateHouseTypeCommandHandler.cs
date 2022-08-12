using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.CreateHouseType;

public class CreateHouseTypeCommandHandler : IRequestHandler<CreateHouseTypeCommand, long>
{
    private readonly IPropertySalesDbContext _dbContext;

    public CreateHouseTypeCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<long> Handle(CreateHouseTypeCommand request, CancellationToken cancellationToken)
    {
        var typeCopy = await _dbContext.HouseTypes
            .AnyAsync(houseType => houseType.Name == request.Name, cancellationToken);

        if (typeCopy)
            throw new RecordExistsException(request.Name);

        var houseType = new Domain.HouseType()
        {
            Name = request.Name
        };

        await _dbContext.HouseTypes.AddAsync(houseType);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return houseType.Id;
    }
}