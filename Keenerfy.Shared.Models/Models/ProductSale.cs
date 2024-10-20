using Microsoft.EntityFrameworkCore;

namespace Keenerfy.Models;

[PrimaryKey(nameof(Product_id), nameof(Sale_id))]
public class ProductSale
{
    public int Product_id { get; set; }
    public int Sale_id { get; set; }
    public int Quantity { get; set; }

}
