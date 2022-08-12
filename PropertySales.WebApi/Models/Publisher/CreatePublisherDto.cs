using AutoMapper;
using PropertySales.Application.CommandsQueries.Publisher.Commands.CreatePublisher;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Publisher;

public class CreatePublisherDto : IMapWith<CreatePublisherCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePublisherDto, CreatePublisherCommand>()
            .ForMember(c => c.Name,
                opt => opt.MapFrom(d => d.Name));
    }
}