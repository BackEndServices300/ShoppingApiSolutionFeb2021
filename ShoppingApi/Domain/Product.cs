using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int NumberAvailable { get; set; }
        public bool Available { get; set; }
        public DateTime AddedToInventoryOn { get; set; }
    }
}
