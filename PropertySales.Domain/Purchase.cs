namespace PropertySales.Domain;

public class Purchase
{
    public long Id { get; set; }
    public DateTime? BuyTime { get; set; }

    public User User { get; set; }
    public House House { get; set; }
}