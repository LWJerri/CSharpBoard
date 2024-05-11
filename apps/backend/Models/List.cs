using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
  [Table("lists")]
  public class List
  {
    [Column("id")]
    [Required]
    public required string Id { get; set; }

    [Column("name")]
    [Required]
    public required string Name { get; set; }

    [Column("created_at")]
    [Required]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    [Required]
    public DateTime UpdatedAt { get; set; }

    public ICollection<Task> Tasks { get; } = new List<Task>();
  }
}