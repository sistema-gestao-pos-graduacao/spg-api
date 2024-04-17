using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPG.Domain.Enums;

namespace SPG.Domain.Model
{
  public class AvaliableTimeModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Teacher")]
    public int TeacherId { get; set; }

    public TeacherModel? Teacher { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    public string Time { get; set; } = string.Empty;
    
    [Required]
    public WeekDaysEnum WeekDays { get; set; }
    
  }
}
