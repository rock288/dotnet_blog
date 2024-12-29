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
    public class SQLArticleRepository: IArticleRepository
    {
        private readonly AppDbContext dbContext;
        public SQLArticleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await dbContext.Articles.ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int ArticleId)
        {
            return await dbContext.Articles.FirstOrDefaultAsync(o => o.ArticleId == ArticleId);
        }

        public async Task<Article> CreateAsync(Article Article)
        {
            await dbContext.Articles.AddAsync(Article);
            await dbContext.SaveChangesAsync();
            return Article;
        }

        public async Task<Article?> UpdateAsync(int ArticleId, Article Article)
        {
            var existingArticle = await dbContext.Articles.FirstOrDefaultAsync(o => o.ArticleId == ArticleId);
            if (existingArticle == null)
            {
                return null;
            }
            await dbContext.SaveChangesAsync();
            await dbContext.Articles.AddAsync(Article);
            return existingArticle;
        }

        public async Task<Article?> DeleteAsync(int ArticleId)
        {
            var existingArticle = await dbContext.Articles.FirstOrDefaultAsync(o => o.ArticleId == ArticleId);
            if (existingArticle == null)
            {
                return null;
            }
            // Delete
            dbContext.Articles.Remove(existingArticle);
            await dbContext.SaveChangesAsync();
            return existingArticle;
        }
    }
}