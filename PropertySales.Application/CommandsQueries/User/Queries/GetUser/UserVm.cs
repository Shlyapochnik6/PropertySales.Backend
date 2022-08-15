using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.User.Queries.GetUser;

public class UserVm : IMapWith<Domain.User>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public long Balance { get; set; }
    public List<Domain.Purchase> Purchases { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.User, UserVm>()
            .ForMember(user => user.UserName,
                opt => opt.MapFrom(user => user.UserName))
            .ForMember(user => user.Email,
                opt => opt.MapFrom(user => user.Email))
            .ForMember(user => user.Balance,
                opt => opt.MapFrom(user => user.Balance))
            .ForMember(user => user.Purchases,
                opt => opt.MapFrom(user => user.Purchases));
    }
}