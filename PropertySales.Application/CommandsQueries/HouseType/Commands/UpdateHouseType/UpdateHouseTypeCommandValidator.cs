using FluentValidation;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.UpdateHouseType;

public class UpdateHouseTypeCommandValidator : AbstractValidator<UpdateHouseTypeCommand>
{
    public UpdateHouseTypeCommandValidator()
    {
        RuleFor(houseType => houseType.Id).NotEmpty();
        RuleFor(houseType => houseType.Name).NotEmpty().MaximumLength(255);
    }
}