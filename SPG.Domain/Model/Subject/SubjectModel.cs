using System.ComponentModel.DataAnnotations;

namespace SPG.Domain.Model
{
  public class SubjectModel
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Name { get; set; } = string.Empty;
  }
}
