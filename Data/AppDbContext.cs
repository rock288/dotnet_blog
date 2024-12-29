using Microsoft.EntityFrameworkCore;
using Rock288.API.Models.Domain;

namespace Rock288.API.Data 
{
 public class AppDbContext : DbContext
    {
        public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public required DbSet<Category> Categories { get; set; }
        public required DbSet<Article> Articles { get; set; }

        // override protected void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     // seed data for category
        //     var categories = new List<Category>()
        //     {
        //         new Category() { Name = "React",},
        //         new Category() { Name = "C#",},
        //         new Category() { Name = "Golang",}
        //     }; 
        // }
    }
}