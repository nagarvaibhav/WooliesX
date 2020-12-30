using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesX.DTO;
using WooliesX.Provider;
using WooliesX.Services;
using WooliesX.Utility;

namespace WooliesX.Tests.Services
{
    public class ProductServiceTests
    {
        private IProductDataProvider _productDataProvider;
        private ProductService _productService;

        [SetUp]
        public void SetUp()
        {
            _productDataProvider = Substitute.For<IProductDataProvider>();
            _productService = new ProductService(_productDataProvider);
        }

        [Test]
        public async Task SortProduct_Should_Return_Sorted_Products_BasedOn_SortOptions()
        {
            var products = MockDataProvider.GetProducts();
            _productDataProvider.GetProducts().Returns(products);

            var resultLow = (await _productService.SortProduct(Constants.Low)).ToList();
            Assert.AreEqual(3, resultLow.Count);
            Assert.AreEqual(43, resultLow[0].Price);
            Assert.AreEqual("Test Product C", resultLow[0].Name);
            var resultHigh = (await _productService.SortProduct(Constants.High)).ToList();
            Assert.AreEqual(3, resultHigh.Count);
            Assert.AreEqual(51, resultHigh[0].Price);
            Assert.AreEqual("Test Product B", resultHigh[0].Name);
            var resultAscending = (await _productService.SortProduct(Constants.Ascending)).ToList();
            Assert.AreEqual(3, resultAscending.Count);
            Assert.AreEqual(45, resultAscending[0].Price);
            Assert.AreEqual("Test Product A", resultAscending[0].Name);
            var resultDescending = (await _productService.SortProduct(Constants.Descending)).ToList();
            Assert.AreEqual(3, resultDescending.Count);
            Assert.AreEqual(43, resultDescending[0].Price);
            Assert.AreEqual("Test Product C", resultDescending[0].Name);
        }

        [Test]
        public async Task SortProduct_Should_Return_Sorted_Products_BasedOn_Recommended_Products()
        {

            var products = MockDataProvider.GetProducts();
            _productDataProvider.GetProducts().Returns(products);

            var shoppersHistory = MockDataProvider.GetShoppersHistory();
            _productDataProvider.GetShoppersHistory().Returns(shoppersHistory);

            var result = (await _productService.SortProduct(Constants.Recommended)).ToList();
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(45, result[0].Price);
            Assert.AreEqual("Test Product A", result[0].Name);
            Assert.AreEqual(51, result[1].Price);
            Assert.AreEqual("Test Product B", result[1].Name);
            Assert.AreEqual(43, result[2].Price);
            Assert.AreEqual("Test Product C", result[2].Name);
        }

        [Test]
        public void GetTrolleyTotal_Should_Return_TrolleyTotal_Considering_Specials_For_Valid_Request()
        {
            var trolleyRequest = MockDataProvider.GetTrolleyRequest(5, 3);

            var result = _productService.GetTrolleyTotal(trolleyRequest);
            Assert.AreEqual(102, result);
        }

        [Test]
        public void GetTrolleyTotal_Should_Return_TrolleyTotal_Ignoring_Specials_If_ProductQuantity_IsLessThan_Special_Quantity()
        {
            var trolleyRequest = MockDataProvider.GetTrolleyRequest(2, 3);

            var result = _productService.GetTrolleyTotal(trolleyRequest);
            Assert.AreEqual(100, result);
        }

        [Test]
        public void GetTrolleyTotal_Should_Return_TrolleyTotal_Ignoring_Specials_If_Special_Is_NotPresent()
        {
            var trolleyRequest = MockDataProvider.GetTrolleyRequest(2, 3, true);

            var result = _productService.GetTrolleyTotal(trolleyRequest);
            Assert.AreEqual(100, result);
        }

        [TestCase(null)]
        [TestCase("Product")]
        [TestCase("Quantity")]
        public void GetTrolleyTotal_Should_Throw_Exception_ForInvalid_Request_When_Request_Is_Null(string paramName)
        {
            var trolleyRequest = MockDataProvider.GetTrolleyRequest(2, 3, true);
            if (string.Compare(paramName, "Product", true) == 0)
                trolleyRequest.Products = null;
            else
                trolleyRequest.Quantities = null;

            var result = Assert.Throws<Exception>(() => _productService.GetTrolleyTotal(trolleyRequest));
            Assert.AreEqual("Invalid Request", result.Message);
        }

        [Test]
        public void GetTrolleyTotal_Should_Throw_Exception_ForInvalid_Request_When_No_ProductInfo_Present()
        {
            var trolleyRequest = MockDataProvider.GetTrolleyRequest(2, 3);
            trolleyRequest.Products = new List<TrolleyProduct>();
            var result = Assert.Throws<Exception>(() => _productService.GetTrolleyTotal(trolleyRequest));
            Assert.AreEqual("Error in calculating trolley price.Product is missing", result.Message);
        }

        [Test]
        public void GetTrolleyTotal_Should_Throw_Exception_ForInvalid_Request_When_No_QuantityInfo_Present()
        {
            var trolleyRequest = MockDataProvider.GetTrolleyRequest(2, 3);
            trolleyRequest.Quantities = new List<ProductQuantity>();
            var result = Assert.Throws<Exception>(() => _productService.GetTrolleyTotal(trolleyRequest));
            Assert.AreEqual("Error in calculating trolley price.Quantity is missing", result.Message);
        }

        [Test]
        public void GetTrolleyTotal_Should_Throw_Exception_ForInvalid_Request_When_ProductName_Does_Not_Match_InQuantity()
        {
            var trolleyRequest = MockDataProvider.GetTrolleyRequest(2, 3);
            trolleyRequest.Quantities.First().Name = "Test";
            var result = Assert.Throws<Exception>(() => _productService.GetTrolleyTotal(trolleyRequest));
            Assert.AreEqual("Error in calculating trolley price.Quantity is missing for product: " + trolleyRequest.Products.First().Name, result.Message);
        }
    }
}
