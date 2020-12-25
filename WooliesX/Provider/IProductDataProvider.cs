using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.DTO;

namespace WooliesX.Provider
{
    public interface IProductDataProvider
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<ShoppersHistory>> GetShoppersHistory();
    }
}
