using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.DTO;
using WooliesX.Provider;
using WooliesX.Utility;

namespace WooliesX.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDataProvider _productDataProvider;

        public ProductService(IProductDataProvider productDataProvider)
        {
            _productDataProvider = productDataProvider;
        }

        public async Task<IEnumerable<Product>> SortProduct(string sortOption)
        {
            var products = await _productDataProvider.GetProducts();
            switch (sortOption.ToLower())
            {
                case Constants.Low:
                    products = products.ToList().OrderBy(x => x.Price);
                    break;
                case Constants.High:
                    products = products.ToList().OrderByDescending(x => x.Price);
                    break;
                case Constants.Ascending:
                    products = products.ToList().OrderBy(x => x.Name);
                    break;
                case Constants.Descending:
                    products = products.ToList().OrderByDescending(x => x.Name);
                    break;
                case Constants.Recommended:
                    var shopersList = await _productDataProvider.GetShoppersHistory();
                    var recommendedProducts = shopersList.SelectMany(x => x.Products).GroupBy(x => x.Name).OrderByDescending(x => x.Count()).Select(x => new Product { Name = x.First().Name, Price = x.First().Price }).ToList();
                    var reaminingProducts = products.Where(x => !recommendedProducts.Contains(x, new ProductComparer()));
                    recommendedProducts.AddRange(reaminingProducts);
                    return recommendedProducts;
                default:
                    products = null;
                    break;
            }
            return products;
        }
    }
}
