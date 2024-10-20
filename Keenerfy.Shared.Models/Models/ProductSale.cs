using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keenerfy.Models
{
    [PrimaryKey(nameof(Product_id), nameof(Sale_id))]
    public class ProductSale
    {
        public int Product_id { get; set; }
        public int Sale_id { get; set; }
        public int Quantity { get; set; }

    }
}
