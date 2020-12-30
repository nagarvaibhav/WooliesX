using System;
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
                    products = products.OrderBy(x => x.Price);
                    break;
                case Constants.High:
                    products = products.OrderByDescending(x => x.Price);
                    break;
                case Constants.Ascending:
                    products = products.OrderBy(x => x.Name);
                    break;
                case Constants.Descending:
                    products = products.OrderByDescending(x => x.Name);
                    break;
                case Constants.Recommended:
                    var shopersList = await _productDataProvider.GetShoppersHistory();
                    var recommendedProducts = shopersList.SelectMany(x => x.Products)
                                                .GroupBy(x => x.Name)
                                                .OrderByDescending(x => x.Sum(y => y.Quantity))
                                                .Select(x => new Product { Name = x.First().Name, Price = x.First().Price }).ToList();

                    var reaminingProducts = products.Where(x => !recommendedProducts.Contains(x, new ProductComparer()));
                    recommendedProducts.AddRange(reaminingProducts);
                    return recommendedProducts;
                default:
                    products = null;
                    break;
            }
            return products;
        }

        public decimal GetTrolleyTotal(TrolleyRequest request)
        {
            if(request == null || request.Products == null || request.Quantities == null)
                throw new Exception("Invalid Request");

            var product = request.Products.FirstOrDefault();
            if (product == null)
                throw new Exception("Error in calculating trolley price.Product is missing");

            var productQuantity = request.Quantities.FirstOrDefault();
            if (productQuantity == null)
                throw new Exception("Error in calculating trolley price.Quantity is missing");

            if (string.Compare(product.Name, productQuantity.Name, false) != 0)
                throw new Exception("Error in calculating trolley price.Quantity is missing for product: " + product.Name);

            int specialQuantity = 0;
            int specialTotal = 0;

            if (request.Specials!= null && request.Specials.Any())
            {
                var special = request.Specials.FirstOrDefault();
                if (special != null && special.Quantities != null && special.Quantities.Any())
                {
                    specialQuantity = special.Quantities.Where(x => x.Name.Equals(product.Name)).Select(x => x.Quantity).First();
                    specialTotal = special.Total;
                }
            }
            if (specialQuantity < productQuantity.Quantity && specialQuantity > 0)
            {
                return ((productQuantity.Quantity % specialQuantity * product.Price)) + specialTotal * Convert.ToInt32((productQuantity.Quantity / specialQuantity));
            }
            else
            {
                return productQuantity.Quantity * product.Price;
            }
        }
    }
}
