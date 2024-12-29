using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rock288.API.Models.Domain
{
  public class Category
  {
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(1000)]
    public required string Name { get; set; }

    // Navigation property: A category can have many articles
    // public ICollection<Article> Articles { get; set; } = new List<Article>();

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; } // Auto-generated timestamp for creation

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; } // Auto-generated timestamp for updates
  }
}
