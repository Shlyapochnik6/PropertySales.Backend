using AutoMapper;
using PropertySales.Application.Common.Mappings;
using PropertySales.Domain;

namespace PropertySales.Application.CommandsQueries.HouseType.Queries.GetHouseType;

public class HouseTypeVm : IMapWith<Domain.HouseType>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Domain.House> Houses { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.HouseType, HouseTypeVm>()
            .ForMember(h => h.Id,
                opt => opt.MapFrom(d => d.Id))
            .ForMember(h => h.Name,
                opt => opt.MapFrom(d => d.Name))
            .ForMember(h => h.Houses,
                opt => opt.MapFrom(d => d.Houses));
    }
}