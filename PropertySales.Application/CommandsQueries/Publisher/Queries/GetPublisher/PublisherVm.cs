using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.Publisher.Queries.GetPublisher;

public class PublisherVm : IMapWith<Domain.Publisher>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Domain.House> Houses { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Publisher, PublisherVm>()
            .ForMember(p => p.Id,
                opt => opt.MapFrom(d => d.Id))
            .ForMember(p => p.Name,
                opt => opt.MapFrom(d => d.Name))
            .ForMember(p => p.Houses,
                opt => opt.MapFrom(d => d.Houses));
    }
}