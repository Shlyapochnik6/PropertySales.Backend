using AutoMapper;
using PropertySales.Application.CommandsQueries.Location.Commands.CreateLocation;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Location;

public class CreateLocationDto : IMapWith<CreateLocationCommand>
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateLocationDto, CreateLocationCommand>()
            .ForMember(c => c.Country,
                opt => opt.MapFrom(d => d.Country))
            .ForMember(c => c.City,
                opt => opt.MapFrom(d => d.City))
            .ForMember(c => c.Street,
                opt => opt.MapFrom(d => d.Street));
    }
}