using AutoMapper;
using PropertySales.Application.CommandsQueries.Purchase.Commands.CreatePurchase;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Purchase;

public class CreatePurchaseDto : IMapWith<CreatePurchaseCommand>
{
    public long? HouseId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePurchaseDto, CreatePurchaseCommand>()
            .ForMember(c => c.HouseId,
                opt => opt.MapFrom(d => d.HouseId));
    }
}