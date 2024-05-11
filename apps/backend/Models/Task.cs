using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
  public enum PriorityEnum
  {
    LOW,
    NORMAL,
    HIGH,
    CRITICAL
  }

  [Table("tasks")]
  public class Task
  {
    [Column("id")]
    [Required]
    public required string Id { get; set; }

    [Column("title")]
    [Required]
    public required string Title { get; set; }

    [Column("description")]
    [Required]
    public required string Description { get; set; }

    [Column("due_at")]
    [Required]
    public DateTime DueAt { get; set; }

    [Column("priority")]
    [Required]
    [EnumDataType(typeof(PriorityEnum))]
    public PriorityEnum Priority { get; set; }

    [Column("created_at")]
    [Required]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    [Required]
    public DateTime UpdatedAt { get; set; }

    [Column("list_id")]
    [Required]
    public required string ListId { get; set; }

    public List List { get; set; } = null!;
  }
}