using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Controllers;
using WooliesX.DTO;
using WooliesX.Services;

namespace WooliesX.Tests.Controller
{
    public class ProductControllerTests
    {
        private IProductService _productService;
        private ProductController _productController;
        private ILogger<ProductController> _logger;

        [SetUp]
        public void SetUp()
        {
            _productService = Substitute.For<IProductService>();
            _logger = Substitute.For<ILogger<ProductController>>();
            _productController = new ProductController(_productService, _logger);
        }

        [TestCase("Test")]
        public async Task GetProduct_Should_Return_SucessFull_Response(string sortOption)
        {
            var products = MockDataProvider.GetProducts();
            _productService.SortProduct(sortOption).Returns(products);
            var result = await _productController.Get(sortOption) as OkObjectResult;
            var productsResponse = result.Value as List<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(productsResponse);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
            Assert.AreEqual(3, productsResponse.Count);
        }


        [Test]
        public async Task GetProduct_Should_Return_BadRequest_Response_For_Empty_SortOption()
        {
            var result = await _productController.Get("") as BadRequestObjectResult;
            var productsResponse = result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(productsResponse);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            Assert.AreEqual("Empty Sort Option", productsResponse);
        }

        [Test]
        public async Task GetProduct_Should_Return_BadRequest_Response_For_Invalid_SortOption()
        {
            List<Product> products = null;
            _productService.SortProduct("test").Returns(products);
            var result = await _productController.Get("test") as BadRequestObjectResult;
            var productsResponse = result.Value;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsNotNull(productsResponse);
            Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
            Assert.AreEqual("Invalid Sort Option. Valid values are Low,High,ascending,descending,recommended", productsResponse);
        }
    }
}
