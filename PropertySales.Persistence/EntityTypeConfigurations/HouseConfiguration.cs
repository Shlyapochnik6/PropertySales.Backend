using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertySales.Domain;

namespace PropertySales.Persistence.EntityTypeConfigurations;

public class HouseConfiguration : IEntityTypeConfiguration<House>
{
    public void Configure(EntityTypeBuilder<House> builder)
    {
        builder.HasKey(house => house.Id);
        builder.HasIndex(house => house.Id).IsUnique();

        builder.HasIndex(house => house.Name).IsUnique();
        
        builder.Property(house => house.Name).IsRequired().HasMaxLength(255);
        builder.Property(house => house.Description).IsRequired().HasMaxLength(600);
        builder.Property(house => house.Material).IsRequired().HasMaxLength(255);
        
        builder.Property(house => house.Price).IsRequired();
        builder.Property(house => house.FloorArea).IsRequired();
        builder.Property(house => house.YearBuilt).IsRequired();
        
        builder.HasOne(house => house.HouseType)
            .WithMany(houseType => houseType.Houses);
        builder.HasOne(house => house.Location)
            .WithMany(location => location.Houses);
        builder.HasOne(house => house.Publisher)
            .WithMany(publisher => publisher.Houses);
        builder.HasMany(house => house.Purchases)
            .WithOne(purchase => purchase.House);
    }
}