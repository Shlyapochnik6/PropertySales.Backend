using FluentValidation;
using MediatR;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.UpdatePublisher;

public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator()
    {
        RuleFor(publisher => publisher.Id).NotEmpty();
        RuleFor(publisher => publisher.Name)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(255);
    }
}