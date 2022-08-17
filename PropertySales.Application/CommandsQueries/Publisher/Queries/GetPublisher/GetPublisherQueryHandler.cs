using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Publisher.Queries.GetPublisher;

public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, PublisherVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Publisher> _cacheManager;

    public GetPublisherQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper,
        ICacheManager<Domain.Publisher> cacheManager)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _cacheManager = cacheManager;
    }
    
    public async Task<PublisherVm> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
    {
        var publisherQuery = async () => await _dbContext.Publishers
            .Include(h => h.Houses)
            .FirstOrDefaultAsync(publisher => publisher.Id == request.Id, cancellationToken);

        var publisher = await _cacheManager.GetOrSetCacheValue(request.Id, publisherQuery);
        
        return _mapper.Map<PublisherVm>(publisher);
    }
}