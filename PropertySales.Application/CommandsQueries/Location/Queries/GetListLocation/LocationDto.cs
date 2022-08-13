using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.Location.Queries.GetListLocation;

public class LocationDto : IMapWith<Domain.Location>
{
    public long Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Location, LocationDto>()
            .ForMember(l => l.Id,
                opt => opt.MapFrom(d => d.Id))
            .ForMember(l => l.Country,
                opt => opt.MapFrom(d => d.Country))
            .ForMember(l => l.City,
                opt => opt.MapFrom(d => d.City))
            .ForMember(l => l.Street,
                opt => opt.MapFrom(d => d.Street));
    }
}