using PropertySales.Persistence.Contexts;

namespace PropertySales.Persistence.Initializers;

public class DbInitializer
{
    public static void Initialize(PropertySalesDbContext context)
    {
        context.Database.EnsureCreated();
    }
}