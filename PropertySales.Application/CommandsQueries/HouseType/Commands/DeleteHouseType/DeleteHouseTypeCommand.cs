using MediatR;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.DeleteHouseType;

public class DeleteHouseTypeCommand : IRequest
{
    public long Id { get; set; }
}