using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorApp1.Api.data;
using BlazorApp1.Api.DTOs;
using BlazorApp1.Api.Entities;
using BlazorApp1.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        public ProductRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET all Products
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        // ------------------------------ ? TODO ??
        // GET all Products
        public async Task<IEnumerable<Product>> GetProducts(string? term)
        {
            IQueryable<Product> queryableProducts;

            if (string.IsNullOrWhiteSpace(term))
            {
                queryableProducts = _dbContext.Products;
            }
            else
            {
                term = term.Trim().ToLower();
                // Applying filters
                queryableProducts = _dbContext.Products.Where(b => b.Name.ToLower().Contains(term) || b.Description.ToLower().Contains(term));
            }

            var products = await queryableProducts.ToListAsync();

            return products;
        }


        // GET existing Products
        public async Task<Product> GetProduct(Guid guid)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == guid);
        }

        // CREATE new Product
        public async Task<Product> AddProduct(ProductDto productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            _dbContext.Products.Add(productEntity);
            await _dbContext.SaveChangesAsync();

            // Return the created product after it has been saved to the database
            return productEntity;
        }

        // UPDATE existing Product
        public async Task UpdateProduct(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        // DELETE existing Product
        public async Task DeleteProduct(Guid guid)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == guid);

            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
            // Handle the case where the product with the given ID is not found if needed
        }
    }
}