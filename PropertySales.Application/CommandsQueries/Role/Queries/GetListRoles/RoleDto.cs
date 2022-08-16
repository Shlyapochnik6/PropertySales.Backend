using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.Role.Queries.GetListRoles;

public class RoleDto : IMapWith<IdentityRole<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IdentityRole<long>, RoleDto>()
            .ForMember(role => role.Id,
                o => o.MapFrom(role => role.Id))
            .ForMember(role => role.Name,
                o => o.MapFrom(role => role.Name));
    }
}