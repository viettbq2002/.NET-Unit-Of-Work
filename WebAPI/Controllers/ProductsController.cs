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
        public async Task<IActionResult> GetListProduct() {
            var products =  await _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct request)
        {
            
            await _productService.CreateProduct(request);
            return Ok();
            
        }
        [HttpDelete("/{productId}")]
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
        [HttpPut("/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProduct product)
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
            return Ok();

        }
    }
}
