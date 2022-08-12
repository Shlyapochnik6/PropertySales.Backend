using MediatR;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.DeletePublisher;

public class DeletePublisherCommand : IRequest
{
    public long Id { get; set; }
}