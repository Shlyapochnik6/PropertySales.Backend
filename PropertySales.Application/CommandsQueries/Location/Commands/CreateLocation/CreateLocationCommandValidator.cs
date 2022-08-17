using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Location.Commands.CreateLocation;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(location => location.Country)
            .NotEmpty().MaximumLength(255);
        RuleFor(location => location.City)
            .NotEmpty().MaximumLength(255);
        RuleFor(location => location.Street)
            .NotEmpty().MaximumLength(255);
    }
}