using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.CreatePublisher;

public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, long>
{
    private readonly IPropertySalesDbContext _dbContext;

    public CreatePublisherCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<long> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = new Domain.Publisher()
        {
            Name = request.Name
        };

        await _dbContext.Publishers.AddAsync(publisher, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return publisher.Id;
    }
}