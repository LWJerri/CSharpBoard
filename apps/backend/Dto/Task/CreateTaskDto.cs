using System.ComponentModel.DataAnnotations;
using Models;

namespace Dto
{
  public partial class CreateTaskDto
  {
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public required string Title { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(3000)]
    public required string Description { get; set; }

    [Required]
    public required DateTime DueAt { get; set; }

    [Required]
    [EnumDataType(typeof(PriorityEnum))]
    public required PriorityEnum Priority { get; set; }
  }
}