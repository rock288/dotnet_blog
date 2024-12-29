
namespace Rock288.API.Models.DTO 
{
    public class UpdateArticleDto
    {
        public required string Title { get; set; }

        public required string Content { get; set; }

        public required int CategoryId { get; set; }
    }
}