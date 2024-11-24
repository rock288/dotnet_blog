using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rock288.API.Models.Domain;

public class Article
{
    [Key]
    public int ArticleId { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Title { get; set; }

    public required string Content { get; set; }

    // Foreign key: An article belongs to one category
    [ForeignKey("Category")]
    public required int CategoryId { get; set; }

    // Navigation property: Link to the related category
    public required Category Category { get; set; }
}
