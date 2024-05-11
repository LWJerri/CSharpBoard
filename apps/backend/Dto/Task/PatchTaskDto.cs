using System.ComponentModel.DataAnnotations;
using Models;

namespace Dto
{
  public partial class PatchTaskDto
  {
    [MinLength(3)]
    [MaxLength(20)]
    public string? Title { get; set; }

    [MinLength(1)]
    [MaxLength(3000)]
    public string? Description { get; set; }

    public DateTime? DueAt { get; set; }

    [EnumDataType(typeof(PriorityEnum))]
    public PriorityEnum? Priority { get; set; }

    public string? ListId { get; set; }
  }
}