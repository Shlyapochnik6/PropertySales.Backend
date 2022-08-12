using FluentValidation;

namespace PropertySales.Application.CommandsQueries.HouseType.Queries.GetHouseType;

public class GetHouseTypeQueryValidator : AbstractValidator<GetHouseTypeQuery>
{
    public GetHouseTypeQueryValidator()
    {
        RuleFor(houseType => houseType.Id).NotEmpty();
    }
}