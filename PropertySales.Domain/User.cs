namespace PropertySales.Domain;

public class User
{
    public decimal Balance { get; set; }

    public List<Purchase> Purchases { get; set; }
}