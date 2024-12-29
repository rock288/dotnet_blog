using System.ComponentModel.DataAnnotations;

namespace Rock288.API.Models.DTO 
{
    public class AddArticleDto
    {
        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        public required string Content { get; set; }

        public required int CategoryId { get; set; }
    }
}