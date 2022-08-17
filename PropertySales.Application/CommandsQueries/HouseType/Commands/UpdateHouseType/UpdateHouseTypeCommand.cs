using MediatR;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.UpdateHouseType;

public class UpdateHouseTypeCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
}