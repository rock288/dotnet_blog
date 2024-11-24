using System.ComponentModel.DataAnnotations;
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
    public ICollection<Article> Articles { get; set; } = new List<Article>();
  }
}
