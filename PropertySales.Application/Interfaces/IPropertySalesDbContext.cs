using Microsoft.EntityFrameworkCore;
using PropertySales.Domain;

namespace PropertySales.Application.Interfaces;

public interface IPropertySalesDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Purchase> Purchases { get; set; }
    DbSet<Publisher> Publishers { get; set; }
    DbSet<Location> Locations { get; set; }
    DbSet<HouseType> HouseTypes { get; set; }
    DbSet<House> Houses { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}