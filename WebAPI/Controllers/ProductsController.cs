using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork.Core.Models;
using UnitOfWork.Infrastructure.DTOs;
using UnitOfWork.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetListProductAsync() {
            var products =  await _productService.GetAllProducts();
            return Ok(products);
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateProductsAsync([FromBody] List<CreateProduct> request)
        {
            await _productService.CreateProductBulk(request);
            return Created(string.Empty, request);
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductAsync(int productId)
        {
            try {
                var product = await _productService.GetProductById(productId);
                return Ok(product);

            } catch (KeyNotFoundException e ) 
            {
                return NotFound(e.Message);

            }

        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductAsync(int productId)
        {
            try
            {
               await _productService.DeleteProduct(productId);
                return NoContent();
            }
            catch (KeyNotFoundException e) {
                return NotFound(e.Message);
            }

        }
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync(int productId, [FromBody] UpdateProduct product)
        {
            try
            {
                await _productService.UpdateProduct(product,productId);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
