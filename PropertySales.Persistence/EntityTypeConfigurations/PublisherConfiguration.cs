using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertySales.Domain;

namespace PropertySales.Persistence.EntityTypeConfigurations;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.HasKey(publisher => publisher.Id);
        builder.HasIndex(publisher => publisher.Id).IsUnique();

        builder.Property(publisher => publisher.Name).IsRequired().HasMaxLength(255);

        builder.HasMany(publisher => publisher.Houses)
            .WithOne(house => house.Publisher);
    }
}