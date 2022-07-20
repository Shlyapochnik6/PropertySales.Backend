namespace PropertySales.Domain;

public class House
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Material { get; set; }
    public decimal Price { get; set; }
    public double FloorArea { get; set; }
    public int YearBuilt { get; set; }
    
    public HouseType HouseType { get; set; }
    public Location Location { get; set; }  
    public Publisher Publisher { get; set; }
    public List<Purchase> Purchases { get; set; }
}