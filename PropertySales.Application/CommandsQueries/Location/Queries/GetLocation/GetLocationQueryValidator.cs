using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Location.Queries.GetLocation;

public class GetLocationQueryValidator : AbstractValidator<GetLocationQuery>
{
    public GetLocationQueryValidator()
    {
        RuleFor(location => location.Id).NotEmpty();
    }
}