using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.Publisher.Queries.GetListPublishers;

public class PublisherDto : IMapWith<Domain.Publisher>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Publisher, PublisherDto>()
            .ForMember(p => p.Id,
                opt => opt.MapFrom(d => d.Id))
            .ForMember(p => p.Name,
                opt => opt.MapFrom(d => d.Name));
    }
}