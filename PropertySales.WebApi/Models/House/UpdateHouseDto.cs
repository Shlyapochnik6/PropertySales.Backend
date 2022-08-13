using AutoMapper;
using PropertySales.Application.CommandsQueries.House.Commands.UpdateHouse;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.House;

public class UpdateHouseDto : IMapWith<UpdateHouseCommand>
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

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateHouseDto, UpdateHouseCommand>()
            .ForMember(c => c.Name,
                opt => opt.MapFrom(d => d.Name))
            .ForMember(c => c.Description,
                opt => opt.MapFrom(d => d.Description))
            .ForMember(c => c.Material,
                opt => opt.MapFrom(d => d.Material))
            .ForMember(c => c.Price,
                opt => opt.MapFrom(d => d.Price))
            .ForMember(c => c.FloorArea,
                opt => opt.MapFrom(d => d.FloorArea))
            .ForMember(c => c.YearBuilt,
                opt => opt.MapFrom(d => d.YearBuilt))
            .ForMember(c => c.HouseTypeId,
                opt => opt.MapFrom(d => d.HouseTypeId))
            .ForMember(c => c.LocationId,
                opt => opt.MapFrom(d => d.LocationId))
            .ForMember(c => c.PublisherId,
                opt => opt.MapFrom(d => d.PublisherId));
    }
}