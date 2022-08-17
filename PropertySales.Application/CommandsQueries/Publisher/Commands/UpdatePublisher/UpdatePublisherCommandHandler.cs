using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Caches;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.UpdatePublisher;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly ICacheManager<Domain.Publisher> _cacheManager;
    
    public UpdatePublisherCommandHandler(IPropertySalesDbContext dbContext,
        ICacheManager<Domain.Publisher> cacheManager)
    {
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<Unit> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var wrongInfo = await _dbContext.Publishers
            .AnyAsync(publisher => publisher.Name == request.Name &&
                                   publisher.Id != request.Id, cancellationToken);
        
        if (wrongInfo)
            throw new RecordExistsException(request.Name);
        
        var publisher = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.Id);

        publisher.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, publisher);
            
        return Unit.Value;
    }
}