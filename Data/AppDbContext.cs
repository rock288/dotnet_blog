using Microsoft.EntityFrameworkCore;
using Rock288.API.Models.Domain;

namespace Rock288.API.Data 
{
 public class AppDbContext : DbContext
    {
        public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Article> Articles { get; set; }
    }
}