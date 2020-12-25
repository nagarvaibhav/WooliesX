using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Options;
using WooliesX.Provider;
using WooliesX.Services;

namespace WooliesX.Tests.Provider
{
    public class ProductDataProviderTests
    {
        private ProductDataProvider _productDataProvider;
        private ILogger<ProductDataProvider> _logger;
        private IHttpClientFactory _httpClient;
        private IOptions<WooliexApiOptions> _wooliexApiOptions;

        [SetUp]
        public void SetUp()
        {
            _httpClient = Substitute.For<IHttpClientFactory>();
            _logger = Substitute.For<ILogger<ProductDataProvider>>();
            _wooliexApiOptions = Microsoft.Extensions.Options.Options.Create(new WooliexApiOptions
            {
                ApiBaseEndpoint = "http://dev-wooliesx-recruitment.azurewebsites.net",
                ProductEndpoint = "test",
                ShoppersListEndpoint = "test",
                Token = "test"
            });
            _productDataProvider = new ProductDataProvider(_httpClient, _logger, _wooliexApiOptions);
        }

        [Test]
        public async Task Getproduct_Should_Return_Response_Sucessfully()
        {
            var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(MockDataProvider.GetProductsString(), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _httpClient.CreateClient().Returns(fakeHttpClient);

            var result = (await _productDataProvider.GetProducts()).ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Test Product A", result[0].Name);
            Assert.AreEqual(99.99, result[0].Price);
            Assert.AreEqual("Test Product B", result[1].Name);
            Assert.AreEqual(101.99, result[1].Price);
            Assert.AreEqual("Test Product C", result[2].Name);
            Assert.AreEqual(10.99, result[2].Price);
        }

        [Test]
        public void Getproduct_Should_Throw_Exception_For_UnSucessfull_Api_Call()
        {
            var fakeHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Fake Error", Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _httpClient.CreateClient().Returns(fakeHttpClient);

            Assert.ThrowsAsync<Exception>(async() => await _productDataProvider.GetProducts());
        }
    }
}
