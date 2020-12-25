using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WooliesX.Services;

namespace WooliesX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(string sortOption)
        {
            try
            {
                if (string.IsNullOrEmpty(sortOption))
                    return BadRequest("Empty Sort Option");
                var result = await _productService.SortProduct(sortOption);
                if (result == null)
                {
                    return BadRequest("Invalid Sort Option. Valid values are Low,High,ascending,descending,recommended");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getting products");
                return StatusCode(500, "error is getting products");
            }
        }
    }
}
