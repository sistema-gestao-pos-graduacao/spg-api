using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class ClassModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Curriculum")]
    public int CurriculumId { get; set; }

    public CurriculumModel? Curriculum { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
  }
}
