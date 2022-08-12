using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.CreatePublisher;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(publisher => publisher.Name)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(255);
    }
}