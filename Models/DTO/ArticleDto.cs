using System.ComponentModel.DataAnnotations;
using Rock288.API.Models.Domain;

namespace Rock288.API.Models.DTO 
{
    public class ArticleDto
    {
        [Key]
        public int ArticleId { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        public required string Content { get; set; }

        public required int CategoryId { get; set; }

        // Navigation property: Link to the related category
        public Category Category { get; set; }
    }
}