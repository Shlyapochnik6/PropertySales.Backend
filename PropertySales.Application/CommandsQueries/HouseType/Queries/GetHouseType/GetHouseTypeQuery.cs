using MediatR;

namespace PropertySales.Application.CommandsQueries.HouseType.Queries.GetHouseType;

public class GetHouseTypeQuery : IRequest<HouseTypeVm>
{
    public long Id { get; set; }
}