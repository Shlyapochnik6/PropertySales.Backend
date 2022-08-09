using PropertySales.Persistence.Contexts;

namespace PropertySales.Persistence.Initializers;

public class LogDbInitializer
{
    public static void Initialize(LogDbContext dbContext) =>
        dbContext.Database.EnsureCreated();
}