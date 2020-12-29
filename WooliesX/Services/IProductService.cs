using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.DTO;

namespace WooliesX.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> SortProduct(string sortOption);
        decimal GetTrolleyTotal(TrolleyRequest request);
    }
}
