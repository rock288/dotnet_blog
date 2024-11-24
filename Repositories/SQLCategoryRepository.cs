using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Rock288.API.Data;
using Rock288.API.Models.Domain;
using Rock288.API.Models.DTO;

namespace Rock288.API.Repositories
{
    public class SQLCategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext dbContext;
        public SQLCategoryRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int categoryId)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(o => o.CategoryId == categoryId);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateAsync(int categoryId, Category category)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(o => o.CategoryId == categoryId);
            if (existingCategory == null)
            {
                return null;
            }
            existingCategory.Name = category.Name;
            await dbContext.SaveChangesAsync();
            await dbContext.Categories.AddAsync(category);
            return existingCategory;
        }

        public async Task<Category?> DeleteAsync(int categoryId)
        {
            var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(o => o.CategoryId == categoryId);
            if (existingCategory == null)
            {
                return null;
            }
            // Delete
            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}