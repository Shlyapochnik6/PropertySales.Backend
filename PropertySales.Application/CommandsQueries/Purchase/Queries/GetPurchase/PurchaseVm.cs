using AutoMapper;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.Application.CommandsQueries.Purchase.Queries.GetPurchase;

public class PurchaseVm : IMapWith<Domain.Purchase>
{
    public long Id { get; set; }
    public DateTime BuyTime { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public Domain.House House { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Purchase, PurchaseVm>()
            .ForMember(purchase => purchase.Id,
                opt => opt.MapFrom(purchase => purchase.Id))
            .ForMember(purchase => purchase.BuyTime,
                opt => opt.MapFrom(purchase => purchase.BuyTime))
            .ForMember(purchase => purchase.UserName,
                opt => opt.MapFrom(purchase => purchase.User.UserName))
            .ForMember(purchase => purchase.Email,
                opt => opt.MapFrom(purchase => purchase.User.Email))
            .ForMember(purchase => purchase.House,
                opt => opt.MapFrom(purchase => purchase.House));
    }
}