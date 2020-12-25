using System.Collections.Generic;

namespace WooliesX.DTO
{
    public class ShoppersHistory
    {
        public IEnumerable<Product> Products { get; set; }
        public int CustomerId { get; set; }
    }
}
