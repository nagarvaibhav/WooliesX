using System.Collections.Generic;
using WooliesX.DTO;

namespace WooliesX.Utility
{
    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product source, Product destination)
        {
            return string.Compare(source.Name, destination.Name, false) == 0;
        }

        public int GetHashCode(Product obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
