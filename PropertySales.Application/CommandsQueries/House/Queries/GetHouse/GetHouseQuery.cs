using MediatR;

namespace PropertySales.Application.CommandsQueries.House.Queries.GetHouse;

public class GetHouseQuery : IRequest<HouseVm>
{
    public long Id { get; set; }
}