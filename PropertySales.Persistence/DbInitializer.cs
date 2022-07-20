namespace PropertySales.Persistence;

public class DbInitializer
{
    public static void Initialize(PropertySalesDbContext context)
    {
        context.Database.EnsureCreated();
    }
}