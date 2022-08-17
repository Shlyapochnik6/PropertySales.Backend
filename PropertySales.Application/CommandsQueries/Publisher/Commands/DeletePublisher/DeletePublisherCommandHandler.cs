using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.DeletePublisher;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly ICacheManager<Domain.Publisher> _cacheManager;
    
    public DeletePublisherCommandHandler(IPropertySalesDbContext dbContext,
        ICacheManager<Domain.Publisher> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.Id);
        
        _dbContext.Publishers.Remove(publisher);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.RemoveCacheValue(request.Id);
        
        return Unit.Value;
    }
}