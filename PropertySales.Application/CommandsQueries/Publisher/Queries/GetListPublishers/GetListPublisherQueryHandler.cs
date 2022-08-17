using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Publisher.Queries.GetListPublishers;

public class GetListPublisherQueryHandler : IRequestHandler<GetListPublisherQuery, GetListPublisherVm>
{
    private readonly IPropertySalesDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetListPublisherQueryHandler(IPropertySalesDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetListPublisherVm> Handle(GetListPublisherQuery request, CancellationToken cancellationToken)
    {
        var publishers = await _dbContext.Publishers
            .ProjectTo<PublisherDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new GetListPublisherVm() { Publishers = publishers };
    }
}