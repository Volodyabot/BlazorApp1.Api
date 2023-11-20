using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Api.DTOs;
using BlazorApp1.Api.Entities;

namespace BlazorApp1.Api.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(Guid guid);
        Task<Product> AddProduct(ProductDto productDto);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Guid guid);

        // ? TODO ??? 
        Task<IEnumerable<Product>> GetProducts(string? term);
    }
}