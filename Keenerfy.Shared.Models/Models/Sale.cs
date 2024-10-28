namespace Keenerfy.Models;
public class Sale
{
    public Sale() { }
    public Sale(DateTime date, int quantity, int product, string users) 
    {
        Date = date;
        Quantity = quantity;
        ProductId = product;
        UsersId = users;
    }
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public string UsersId { get; set; }
    public virtual User Users { get; set; }

    public int ProductId { get; set; }  
    public virtual Product Product { get; set; } 

    public int Quantity { get; set; } 
}