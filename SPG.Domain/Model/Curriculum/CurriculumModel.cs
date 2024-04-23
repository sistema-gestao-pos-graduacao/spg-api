using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class CurriculumModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Course")]
    public int CourseId { get; set; } 

    public CourseModel? Course { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
  }
}
