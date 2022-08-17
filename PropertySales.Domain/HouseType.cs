namespace PropertySales.Domain;

public class HouseType
{
    public long Id { get; set; }
    public string Name { get; set; }

    public List<House> Houses { get; set; }
}