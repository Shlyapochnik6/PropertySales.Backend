using AutoMapper;
using PropertySales.Application.CommandsQueries.HouseType.Commands.CreateHouseType;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.HouseType;

public class CreateHouseTypeDto : IMapWith<CreateHouseTypeCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateHouseTypeDto, CreateHouseTypeCommand>()
            .ForMember(c => c.Name,
                opt => opt.MapFrom(d => d.Name));
    }
}