using MediatR;

namespace PropertySales.Application.CommandsQueries.Location.Commands.DeleteLocation;

public class DeleteLocationCommand : IRequest
{
    public long Id { get; set; }
}