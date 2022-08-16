using AutoMapper;
using PropertySales.Application.CommandsQueries.Role.Commands.SetRole;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Role;

public class SetRoleDto : IMapWith<SetRoleCommand>
{
    public long UserId { get; set; }
    public string RoleId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetRoleDto, SetRoleCommand>()
            .ForMember(c => c.UserId,
                o => o.MapFrom(d => d.UserId))
            .ForMember(c => c.RoleId,
                o => o.MapFrom(d => d.RoleId));
    }
}