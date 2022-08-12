using MediatR;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.CreateHouseType;

public class CreateHouseTypeCommand : IRequest<long>
{
    public string Name { get; set; }
}