using Microsoft.AspNetCore.Mvc;
using System.Net;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services.ProductService;







namespace AngularApp1.Server.Controllers
{
    [Route("app/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound("Produsul nu a fost gasit in baza de date!");
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filterProducts")]
        public async Task<IActionResult> FilterProductsAsync(string color = "All", string sizeGiven = "All", float minValue = 0, float maxValue = 100000, string sortType = "")
        {
            try
            {
                List<Product> products = await _productService.FilterProductsAsync(color, sizeGiven, minValue, maxValue, sortType);
                if (!products.Any())
                {
                    return NotFound("Nu exista niciun produs in baza de date cu aceste filtre puse!");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
        {
            try
            {
                await _productService.AddProductAsync(product);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
