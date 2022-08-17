using FluentValidation;

namespace PropertySales.Application.CommandsQueries.House.Queries.GetHouse;

public class GetHouseQueryValidator : AbstractValidator<GetHouseQuery>
{
    public GetHouseQueryValidator()
    {
        RuleFor(house => house.Id).NotEmpty();
    }
}