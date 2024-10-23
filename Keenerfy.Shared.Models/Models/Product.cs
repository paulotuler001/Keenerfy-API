namespace Keenerfy.Models;

public class Product
{

    public Product()
    {

    }
    public Product(string name, string code, string description, float price, byte[] link, int stock)
    {
        Name = name;
        Code = code;
        Description = description;
        Price = price;
        Link = link;
        Stock = stock;
    }

    public int Id {  get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float? Price { get; set; }
    public byte[] Link { get; set; }
    public int Stock { get; set; }
    public virtual ICollection<Sale> Sales { get; set; }
}
