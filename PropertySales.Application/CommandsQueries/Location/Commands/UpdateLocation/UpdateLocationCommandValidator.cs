using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Location.Commands.UpdateLocation;

public class UpdateLocationValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationValidator()
    {
        RuleFor(location => location.Id).NotEmpty();
        RuleFor(location => location.Country).NotEmpty().MaximumLength(255);
        RuleFor(location => location.City).NotEmpty().MaximumLength(255);
        RuleFor(location => location.Street).NotEmpty().MaximumLength(255);
    }
}