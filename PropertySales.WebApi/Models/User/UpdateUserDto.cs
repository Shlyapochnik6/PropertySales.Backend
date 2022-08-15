using AutoMapper;
using PropertySales.Application.Common.Mappings;
using PropertySales.SecureAuth.Commands.UpdateUser;

namespace PropertySales.WebApi.Models.User;

public class UpdateUserDto : IMapWith<Domain.User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
            .ForMember(user => user.UserName,
                o => o.MapFrom(user => user.UserName))
            .ForMember(user => user.Email,
                o => o.MapFrom(user => user.Email))
            .ForMember(user => user.OldPassword,
                o => o.MapFrom(user => user.OldPassword))
            .ForMember(user => user.NewPassword,
                o => o.MapFrom(user => user.NewPassword));
    }
}