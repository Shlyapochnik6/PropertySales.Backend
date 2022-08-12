using MediatR;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Common.Exceptions;
using PropertySales.Application.Interfaces;

namespace PropertySales.Application.CommandsQueries.Publisher.Commands.UpdatePublisher;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Unit>
{
    private readonly IPropertySalesDbContext _dbContext;

    public UpdatePublisherCommandHandler(IPropertySalesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var wrongInfo = await _dbContext.Publishers
            .AnyAsync(publisher => publisher.Name == request.Name &&
                                   publisher.Id != request.Id, cancellationToken);
        
        if (wrongInfo)
            throw new RecordExistsException(request.Name);
        
        var publisher = await _dbContext.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.Id);

        publisher.Name = request.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);
            
        return Unit.Value;
    }
}