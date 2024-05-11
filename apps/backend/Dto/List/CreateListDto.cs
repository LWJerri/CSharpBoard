using System.ComponentModel.DataAnnotations;

namespace Dto
{
  public partial class CreateListDto
  {
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public required string Name { get; set; }
  }
}