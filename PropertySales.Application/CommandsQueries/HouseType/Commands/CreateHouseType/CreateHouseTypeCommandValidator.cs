using FluentValidation;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.CreateHouseType;

public class CreateHouseTypeCommandValidator : AbstractValidator<CreateHouseTypeCommand>
{
    public CreateHouseTypeCommandValidator()
    {
        RuleFor(houseType => houseType.Name)
            .NotEmpty().MaximumLength(255);
    }
}