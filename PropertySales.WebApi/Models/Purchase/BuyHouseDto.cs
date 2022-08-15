using AutoMapper;
using PropertySales.Application.CommandsQueries.Purchase.Commands.BuyHouse;
using PropertySales.Application.Common.Mappings;

namespace PropertySales.WebApi.Models.Purchase;

public class BuyHouseDto : IMapWith<BuyHouseCommand>
{
    public string PublisherName { get; set; }
    public string HouseName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BuyHouseDto, BuyHouseCommand>()
            .ForMember(c => c.PublisherName,
                opt => opt.MapFrom(d => d.PublisherName))
            .ForMember(c => c.HouseName,
            opt => opt.MapFrom(d => d.HouseName));
    }
}