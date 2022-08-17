using AutoMapper;
using PropertySales.Application.CommandsQueries.Role.Commands.UpdateRole;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Role;

public class UpdateRoleDto : IMapWith<UpdateRoleCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateRoleDto, UpdateRoleCommand>()
            .ForMember(c => c.Name,
                o => o.MapFrom(d => d.Name));
    }
}