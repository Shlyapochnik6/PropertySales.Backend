using AutoMapper;
using PropertySales.Application.CommandsQueries.Location.Commands.UpdateLocation;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Location;

public class UpdateLocationDto : IMapWith<UpdateLocationCommand>
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateLocationDto, UpdateLocationCommand>()
            .ForMember(c => c.Country,
                opt => opt.MapFrom(d => d.Country))
            .ForMember(c => c.City,
                opt => opt.MapFrom(d => d.City))
            .ForMember(c => c.Street,
                opt => opt.MapFrom(d => d.Street));
    }
}