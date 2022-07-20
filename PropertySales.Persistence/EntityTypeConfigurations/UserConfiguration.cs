using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertySales.Domain;

namespace PropertySales.Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.Balance).IsRequired();
        
        builder.HasMany(user => user.Purchases)
            .WithOne(purchase => purchase.User);
    }
}