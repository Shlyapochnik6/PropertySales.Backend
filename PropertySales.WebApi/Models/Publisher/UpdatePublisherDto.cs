using AutoMapper;
using PropertySales.Application.CommandsQueries.Publisher.Commands.CreatePublisher;
using PropertySales.Application.CommandsQueries.Publisher.Commands.UpdatePublisher;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Publisher;

public class UpdatePublisherDto : IMapWith<UpdatePublisherCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePublisherDto, UpdatePublisherCommand>()
            .ForMember(c => c.Name,
                opt => opt.MapFrom(d => d.Name));
    }
}