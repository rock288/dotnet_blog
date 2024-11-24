using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rock288.API.Models.Domain;

namespace Rock288.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int categoryId);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(int categoryId, Category category);
        Task<Category?> DeleteAsync(int categoryId);
    }
}