using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertySales.Domain;

namespace PropertySales.Persistence.EntityTypeConfigurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(purchase => purchase.Id);
        builder.HasIndex(purchase => purchase.Id).IsUnique();
        
        builder.Property(purchase => purchase.Count).HasDefaultValue(1);
        builder.Property(purchase => purchase.TotalPrice).HasDefaultValue(0);
        
        builder.HasOne(purchase => purchase.User)
            .WithMany(user => user.Purchases);
        builder.HasOne(purchase => purchase.House)
            .WithMany(user => user.Purchases);
    }
}