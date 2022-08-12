using FluentValidation;

namespace PropertySales.Application.CommandsQueries.Publisher.Queries.GetPublisher;

public class GetPublisherQueryValidator : AbstractValidator<GetPublisherQuery>
{
    public GetPublisherQueryValidator()
    {
        RuleFor(publisher => publisher.Id).NotEmpty();
    }
}