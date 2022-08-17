using MediatR;

namespace PropertySales.Application.CommandsQueries.House.Commands.UpdateHouse;

public class UpdateHouseCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Material { get; set; }
    public decimal Price { get; set; }
    public double FloorArea { get; set; }
    public int YearBuilt { get; set; }

    public long HouseTypeId { get; set; }
    public long LocationId { get; set; }
    public long PublisherId { get; set; }
}