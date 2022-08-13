using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.House.Queries.GetListHouses;

public class HouseDto : IMapWith<Domain.House>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Material { get; set; }
    public decimal Price { get; set; }
    public double FloorArea { get; set; }
    public int YearBuilt { get; set; }

    public Domain.Publisher Publisher { get; set; }
    public Domain.HouseType HouseType { get; set; }
    public Domain.Location Location { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.House, HouseDto>()
            .ForMember(h => h.Id,
                opt => opt.MapFrom(d => d.Id))
            .ForMember(h => h.Name,
                opt => opt.MapFrom(d => d.Name))
            .ForMember(h => h.Description,
                opt => opt.MapFrom(d => d.Description))
            .ForMember(h => h.Material,
                opt => opt.MapFrom(d => d.Material))
            .ForMember(h => h.Price,
                opt => opt.MapFrom(d => d.Price))
            .ForMember(h => h.FloorArea,
                opt => opt.MapFrom(d => d.FloorArea))
            .ForMember(h => h.YearBuilt,
                opt => opt.MapFrom(d => d.YearBuilt))
            .ForMember(h => h.Publisher,
                opt => opt.MapFrom(d => d.Publisher))
            .ForMember(h => h.HouseType,
                opt => opt.MapFrom(d => d.HouseType))
            .ForMember(h => h.Location,
                opt => opt.MapFrom(d => d.Location));
    }
}