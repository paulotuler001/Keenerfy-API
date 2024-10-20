using Keenerfy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keenerfy.Shared.Models.Models;
public class PurchaseOrder
{
    public PurchaseOrder() { }
    public PurchaseOrder(DateTime date, int quantity, Product product)
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
