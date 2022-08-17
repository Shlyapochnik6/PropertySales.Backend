using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertySales.Domain;

namespace PropertySales.Persistence.EntityTypeConfigurations;

public class HouseTypeConfiguration : IEntityTypeConfiguration<HouseType>
{
    public void Configure(EntityTypeBuilder<HouseType> builder)
    {
        builder.HasKey(houseType => houseType.Id);
        builder.HasIndex(houseType => houseType.Id).IsUnique();

        builder.HasIndex(houseType => houseType.Name).IsUnique();
        
        builder.Property(houseType => houseType.Name).IsRequired().HasMaxLength(255);
        
        builder.HasMany(houseType => houseType.Houses)
            .WithOne(house => house.HouseType);
    }
}