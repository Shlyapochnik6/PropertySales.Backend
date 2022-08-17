using FluentValidation;

namespace PropertySales.Application.CommandsQueries.HouseType.Commands.DeleteHouseType;

public class DeleteHouseTypeCommandValidator : AbstractValidator<DeleteHouseTypeCommand>
{
    public DeleteHouseTypeCommandValidator()
    {
        RuleFor(houseType => houseType.Id).NotEmpty();
    }
}