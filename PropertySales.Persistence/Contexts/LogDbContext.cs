using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;
using PropertySales.Domain;
using PropertySales.Persistence.EntityTypeConfigurations;

namespace PropertySales.Persistence.Contexts;

public class LogDbContext : DbContext, ILogDbContext
{
    public DbSet<Log> Logs { get; set; }

    public LogDbContext(DbContextOptions<LogDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new LogConfiguration());
        builder.Entity<Log>().ToTable("log");
        
        base.OnModelCreating(builder);
    }
}