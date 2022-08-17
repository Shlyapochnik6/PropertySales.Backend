using MediatR;

namespace PropertySales.Application.CommandsQueries.Location.Commands.UpdateLocation;

public class UpdateLocationCommand : IRequest
{
    public long Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
}