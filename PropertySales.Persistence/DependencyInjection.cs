using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertySales.Application.Interfaces;
using PropertySales.Persistence.Contexts;

namespace PropertySales.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<PropertySalesDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        var logConnectionString = configuration["DbLogConnection"];
        services.AddDbContext<LogDbContext>(options =>
        {
            options.UseNpgsql(logConnectionString);
        });

        services.AddScoped<IPropertySalesDbContext, PropertySalesDbContext>();
        services.AddScoped<LogDbContext>();

        return services;
    }
}