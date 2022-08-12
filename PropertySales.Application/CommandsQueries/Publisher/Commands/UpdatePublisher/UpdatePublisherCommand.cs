using MediatR;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.UpdatePublisher;

public class UpdatePublisherCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
}