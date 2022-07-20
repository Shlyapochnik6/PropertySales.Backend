using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using PropertySales.Application.Interfaces;
using PropertySales.Domain;
using PropertySales.Persistence.EntityTypeConfigurations;

namespace PropertySales.Persistence;

public class PropertySalesDbContext : DbContext, IPropertySalesDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<HouseType> HouseTypes { get; set; }
    public DbSet<House> Houses { get; set; }

    public PropertySalesDbContext(DbContextOptions<PropertySalesDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
        modelBuilder.ApplyConfiguration(new PublisherConfiguration());
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new HouseTypeConfiguration());
        modelBuilder.ApplyConfiguration(new HouseConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}