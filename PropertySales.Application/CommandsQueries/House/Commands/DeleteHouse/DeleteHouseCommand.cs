using MediatR;

namespace PropertySales.Application.CommandsQueries.House.Commands.DeleteHouse;

public class DeleteHouseCommand : IRequest
{
    public long Id { get; set; }
}