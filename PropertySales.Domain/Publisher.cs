namespace PropertySales.Domain;

public class Publisher
{
    public long Id { get; set; }
    public string Name { get; set; }

    public List<House> Houses { get; set; }
}