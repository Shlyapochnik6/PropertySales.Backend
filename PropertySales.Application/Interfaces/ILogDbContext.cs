using Microsoft.EntityFrameworkCore;
using PropertySales.Domain;

namespace PropertySales.Application.Interfaces;

public interface ILogDbContext
{ 
    DbSet<Log> Logs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}