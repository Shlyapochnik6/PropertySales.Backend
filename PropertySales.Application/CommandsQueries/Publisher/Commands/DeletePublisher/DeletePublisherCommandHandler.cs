using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.DeletePublisher;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;

    public DeletePublisherCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.Id);
        
        _dbContext.Publishers.Remove(publisher);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}