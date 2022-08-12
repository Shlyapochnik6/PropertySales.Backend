using AutoMapper;
using PropertySales.Application.CommandsQueries.HouseType.Commands.UpdateHouseType;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.HouseType;

public class UpdateHouseTypeDto : IMapWith<UpdateHouseTypeCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateHouseTypeDto, UpdateHouseTypeCommand>()
            .ForMember(c => c.Name,
                opt => opt.MapFrom(d => d.Name));
    }
}