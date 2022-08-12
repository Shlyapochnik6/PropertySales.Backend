using MediatR;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.CreatePublisher;

public class CreatePublisherCommand : IRequest<long>
{
    public string Name { get; set; }
}