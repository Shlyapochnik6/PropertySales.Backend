namespace PropertySales.Domain;

public class Location
{
    public long Id { get; set; }
    public string Name { get; set; }

    public List<House> Houses { get; set; }
}