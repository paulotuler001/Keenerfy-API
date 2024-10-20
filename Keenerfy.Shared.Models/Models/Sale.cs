namespace Keenerfy.Models;
public class Sale
{
    public Sale() { }
    public Sale(DateTime date, int quantity, Product product) 
    {
        Date = date;
        Quantity = quantity;
        Product = product;
    }
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public virtual User Users { get; set; }

    public int ProductId { get; set; }  
    public virtual Product Product { get; set; } 

    public int Quantity { get; set; } 
}