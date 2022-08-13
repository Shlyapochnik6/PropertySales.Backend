using MediatR;

namespace PropertySales.Application.CommandsQueries.Location.Commands.CreateLocation;

public class CreateLocationCommand : IRequest<long>
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
}