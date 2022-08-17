using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.DeletePublisher;

public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
{
    public DeletePublisherCommandValidator()
    {
        RuleFor(publisher => publisher.Id).NotEmpty();
    }
}