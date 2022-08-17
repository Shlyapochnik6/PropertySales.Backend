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

        builder.HasOne(purchase => purchase.User)
            .WithMany(user => user.Purchases);
        builder.HasOne(purchase => purchase.House)
            .WithMany(user => user.Purchases);
    }
}