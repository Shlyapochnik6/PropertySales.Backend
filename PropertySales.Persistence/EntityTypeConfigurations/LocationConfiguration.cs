﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertySales.Domain;

namespace PropertySales.Persistence.EntityTypeConfigurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(location => location.Id);
        builder.HasIndex(location => location.Id).IsUnique();

        builder.Property(location => location.Name).IsRequired().HasMaxLength(255);

        builder.HasMany(location => location.Houses)
            .WithOne(house => house.Location);
    }
}