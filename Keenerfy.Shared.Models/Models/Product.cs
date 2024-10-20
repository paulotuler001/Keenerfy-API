using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keenerfy.Models
{
    public class Product
    {
        public int Id {  get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float? Price { get; set; }
        public string Link { get; set; }
        public int Stock_id { get; set; }
    }
}
