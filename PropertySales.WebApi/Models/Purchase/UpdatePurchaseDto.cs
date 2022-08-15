using AutoMapper;
using PropertySales.Application.CommandsQueries.Purchase.Commands.UpdatePurchase;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Purchase;

public class UpdatePurchaseDto : IMapWith<UpdatePurchaseCommand>
{
    public long? HouseId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePurchaseDto, UpdatePurchaseCommand>()
            .ForMember(c => c.HouseId,
                opt => opt.MapFrom(d => d.HouseId));
    }
}