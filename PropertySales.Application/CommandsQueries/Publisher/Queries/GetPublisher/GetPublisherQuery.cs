using MediatR;

namespace PropertySales.Application.CommandsQueries.Publisher.Queries.GetPublisher;

public class GetPublisherQuery : IRequest<PublisherVm>
{
    public long Id { get; set; }
}