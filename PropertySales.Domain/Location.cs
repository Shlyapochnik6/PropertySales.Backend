namespace PropertySales.Domain;

public class Location
{
    public long Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }

    public List<House> Houses { get; set; }
}