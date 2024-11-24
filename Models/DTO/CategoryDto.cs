using System.ComponentModel.DataAnnotations;

namespace Rock288.API.Models.DTO 
{
    public class CategoryDto
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Name { get; set; }

        // Navigation property: A category can have many articles
        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}