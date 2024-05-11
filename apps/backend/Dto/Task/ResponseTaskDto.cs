using System.ComponentModel.DataAnnotations;
using Models;

namespace Dto
{
  public partial class ResponseTaskDto
  {
    [Required]
    public required string Id { get; set; }

    [Required]
    public required string Title { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required DateTime DueAt { get; set; }

    [Required]
    public required PriorityEnum Priority { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }

    [Required]
    public required DateTime UpdatedAt { get; set; }

    [Required]
    public required string ListId { get; set; }
  }
}