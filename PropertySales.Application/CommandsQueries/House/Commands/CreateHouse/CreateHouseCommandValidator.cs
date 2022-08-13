using FluentValidation;

namespace PropertySales.Application.CommandsQueries.House.Commands.CreateHouse;

public class CreateHouseCommandValidator : AbstractValidator<CreateHouseCommand>
{
    public CreateHouseCommandValidator()
    {
        RuleFor(house => house.Name).NotEmpty().MaximumLength(255);
        RuleFor(house => house.Description).NotEmpty().MaximumLength(600);
        RuleFor(house => house.Material).NotEmpty().MaximumLength(255);
        RuleFor(house => house.Price).NotEmpty();
        RuleFor(house => house.FloorArea).NotEmpty();
        RuleFor(house => house.YearBuilt).NotEmpty();
        RuleFor(house => house.PublisherId).NotEmpty();
        RuleFor(house => house.HouseTypeId).NotEmpty();
        RuleFor(house => house.LocationId).NotEmpty();
    }
}