using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rock288.API.Models.Domain;
using Rock288.API.Models.DTO;

namespace Rock288.API.Repositories
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllAsync();
        Task<List<Article>> GetArticleByCategoryAsync(int CategoryId, int pageSize, int pageNumber);
        Task<Article?> GetByIdAsync(int ArticleId);
        Task<Article> CreateAsync(Article Article);
        Task<Article?> UpdateAsync(int ArticleId, Article Article);
        Task<Article?> DeleteAsync(int ArticleId);
    }
}