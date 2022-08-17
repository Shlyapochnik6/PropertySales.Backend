using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.HouseType.Queries.GetListHouseTypes;

public class HouseTypeDto : IMapWith<Domain.HouseType>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.HouseType, HouseTypeDto>()
            .ForMember(h => h.Id,
                opt => opt.MapFrom(d => d.Id))
            .ForMember(h => h.Name,
                opt => opt.MapFrom(d => d.Name));
    }
}