using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Publisher.Queries.GetPublisher;

public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, PublisherVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPublisherQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<PublisherVm> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
    {
        var publisherQuery = await _dbContext.Publishers
            .Include(h => h.Houses)
            .FirstOrDefaultAsync(publisher => publisher.Id == request.Id, cancellationToken);

        return _mapper.Map<PublisherVm>(publisherQuery);
    }
}