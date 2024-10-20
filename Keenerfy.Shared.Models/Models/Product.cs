namespace Keenerfy.Models;

public class Product
{
    public Product(string name, string code, string description, float price, string link, int stock_id)
    {
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        Link = link;
        Stock_id = stock_id;
    }

    public int Id {  get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float? Price { get; set; }
    public string Link { get; set; }
    public int Stock_id { get; set; }
}
