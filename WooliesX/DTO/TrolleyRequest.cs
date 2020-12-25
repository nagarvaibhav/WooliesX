using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesX.DTO
{
    public class TrolleyRequest
    {
        public class TrolleyProduct
        {
            public string Name { get; set; }
            public int Price { get; set; }
        }

        public class ProductQuantity
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }

        public class Special
        {
            public List<ProductQuantity> Quantities { get; set; }
            public int Total { get; set; }
        }

        public List<Product> Products { get; set; }
        public List<Special> Specials { get; set; }
        public List<ProductQuantity> Quantities { get; set; }
    }
}
