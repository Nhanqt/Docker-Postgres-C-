using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsApi.Models;
using ProductsApi.Repositories;
using ProductsApi.Dtos;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.Get(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            Product product = new Product
            {
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                DateCreated = DateTime.Now
            };

            await _productRepository.Add(product);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productRepository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            Product product = new Product
            {
                Name = updateProductDto.Name,
                Price = updateProductDto.Price
            };

            await _productRepository.Update(product);
            return Ok();
        }
    }
}