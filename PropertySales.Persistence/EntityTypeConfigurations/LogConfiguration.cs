using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertySales.Domain;

namespace PropertySales.Persistence.EntityTypeConfigurations;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.HasKey(log => log.Id);
        builder.HasIndex(log => log.Id).IsUnique();

        builder.Property(log => log.Application)
            .IsRequired().HasMaxLength(255)
            .HasColumnName("application");
        
        builder.Property(log => log.Logged)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("logged");
        
        builder.Property(log => log.Level)
            .IsRequired().HasMaxLength(100)
            .HasColumnName("level");
        
        builder.Property(log => log.Message)
            .IsRequired()
            .HasMaxLength(4000)
            .HasColumnName("message");
        
        builder.Property(log => log.Logger)
            .IsRequired()
            .HasMaxLength(4000)
            .HasColumnName("logger");
        
        builder.Property(log => log.Callsite)
            .IsRequired()
            .HasMaxLength(4000)
            .HasColumnName("callsite");
        
        builder.Property(log => log.Exception)
            .IsRequired()
            .HasMaxLength(4000)
            .HasColumnName("exception");
    }
}