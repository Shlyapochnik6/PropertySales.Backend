using MediatR;

namespace PropertySales.Application.CommandsQueries.Location.Queries.GetLocation;

public class GetLocationQuery : IRequest<LocationVm>
{
    public long Id { get; set; }
}