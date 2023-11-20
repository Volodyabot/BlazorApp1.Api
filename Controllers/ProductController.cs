using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Api.DTOs;
using BlazorApp1.Api.Entities;
using BlazorApp1.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorApp1.Api.Controllers
{
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetProducts(string? term)
        {
            var products = await _productRepository.GetProducts(term);
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productRepository.GetProduct(id);

            if (product == null)
            {
                return NotFound(); // 404 Not Found if the product is not found
            }

            return Ok(product);
        }


        // POST: api/products
        [HttpPost()]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(); // 400 Bad Request if the request body is empty
            }

            var createdProduct = await _productRepository.AddProduct(productDto);

            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(); // 400 Bad Request if the provided ID doesn't match the product ID
            }

            await _productRepository.UpdateProduct(product);

            return NoContent(); // 204 No Content on successful update
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productRepository.DeleteProduct(id);

            return NoContent(); // 204 No Content on successful delete
        }
    }
}