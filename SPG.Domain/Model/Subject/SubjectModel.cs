using SPG.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class SubjectModel
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [ForeignKey("Curriculum")]
    public int CurriculumId { get; set; }

    public CurriculumModel? Curriculum { get; set; }
    
    [Required]
    public int Hours { get; set; }

    [ForeignKey("Teacher")]
    public int? TeacherId { get; set; }

    public PersonModel? Teacher { get; set; }

    [StringLength(255)]
    public string Location { get; set; } = string.Empty;

    public int? Building { get; set; }

    public int? Room { get; set; }

    public List<string> Students { get; set; } = [];

    [StringLength(1000)]
    public string Considerations { get; set; } = string.Empty;

    public WeekDaysEnum WeekDay { get; set; }

    [StringLength(2047)]
    public string Syllabus { get; set; } = string.Empty;

    [StringLength(7)]
    public string Color { get; set; } = string.Empty;
  }
}
