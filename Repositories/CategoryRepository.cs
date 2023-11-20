using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp1.Api.data;
using BlazorApp1.Api.Entities;
using BlazorApp1.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Api.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }


        // Retrieves all categories from the database.  GET
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }


        // Retrieves a single category by its unique identifier.    GET
        public async Task<Category> GetCategory(Guid categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }


        // Adds a new category to the database. CREATE
        public async Task AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }


        // Updates an existing category in the database.    UPDATE
        public async Task UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        // Deletes a category from the database by its unique identifier.   DELETE
        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            // Handle the case where the category with the given ID is not found if needed
        }

    }
}