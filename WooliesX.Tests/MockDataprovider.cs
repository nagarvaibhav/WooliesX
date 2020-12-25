using System.Collections.Generic;
using WooliesX.DTO;

namespace WooliesX.Tests
{
    public class MockDataProvider
    {

        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product {Name = "test", Price = 45, Quantity = 1 },
                new Product {Name = "test1", Price = 51, Quantity = 2 },
                new Product {Name = "test2", Price = 43, Quantity = 1 }
            };
        }

        public static string GetProductsString()
        {
            return "[{'name': 'Test Product A','price': 99.99,'quantity': 0},{'name': 'Test Product B','price': 101.99,'quantity': 0},{'name': 'Test Product C','price': 10.99,'quantity': 0}]";
        }

        public static List<ShoppersHistory> GetShoppersHistory()
        {

            var result = new List<ShoppersHistory>();
            result.Add(new ShoppersHistory
            {
                CustomerId = 123,
                Products = new List<Product> {
                new Product {Name = "test", Price = 45, Quantity = 1 },
                new Product {Name = "test1", Price = 51, Quantity = 2 },
                }
            });
            result.Add(new ShoppersHistory
            {
                CustomerId = 23,
                Products = new List<Product> {
                new Product {Name = "test1", Price = 51, Quantity = 2 },
                }
            });
            result.Add(new ShoppersHistory
            {
                CustomerId = 54,
                Products = new List<Product> {
                new Product {Name = "test1", Price = 51, Quantity = 2 },
                }
            });
            return result;
        }
    }
}
