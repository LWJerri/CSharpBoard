using System.ComponentModel.DataAnnotations;

namespace Dto
{
  public partial class ResponseListDto
  {
    [Required]
    public required string Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required DateTime CreatedAt { get; set; }

    [Required]
    public required DateTime UpdatedAt { get; set; }

    [Required]
    public required int Task { get; set; }
  }
}