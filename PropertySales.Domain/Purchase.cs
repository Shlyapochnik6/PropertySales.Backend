namespace PropertySales.Domain;

public class Purchase
{
    public long Id { get; set; }
    public int Count { get; set; }
    public decimal TotalPrice { get; set; }

    public User User { get; set; }
    public House House { get; set; }
}