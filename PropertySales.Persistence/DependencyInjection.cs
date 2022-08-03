using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PropertySales.Application.Interfaces;

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

        services.AddScoped<IPropertySalesDbContext>(provider => 
            provider.GetService<PropertySalesDbContext>());

        return services;
    }
}