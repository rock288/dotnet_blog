using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rock288.API.Models.Domain;

namespace Rock288.API.Repositories
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int ArticleId);
        Task<Article> CreateAsync(Article Article);
        Task<Article?> UpdateAsync(int ArticleId, Article Article);
        Task<Article?> DeleteAsync(int ArticleId);
    }
}