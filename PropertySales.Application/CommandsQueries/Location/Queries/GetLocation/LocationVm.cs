using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.Location.Queries.GetLocation;

public class LocationVm : IMapWith<Domain.Location>
{
    public long Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public List<Domain.House> Houses { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Location, LocationVm>()
            .ForMember(l => l.Id,
                opt => opt.MapFrom(d => d.Id))
            .ForMember(l => l.Country,
                opt => opt.MapFrom(d => d.Country))
            .ForMember(l => l.City,
                opt => opt.MapFrom(d => d.City))
            .ForMember(l => l.Street,
                opt => opt.MapFrom(d => d.Street))
            .ForMember(l => l.Houses,
                opt => opt.MapFrom(d => d.Houses));
    }
}