using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPG.Domain.Model.AvaliableTime;
using SPG.Domain.Model;

namespace SPG.Domain.Model.ScheduledClasses
{
  public class ScheduledClassesModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Subject")]
    public int SubjectId { get; set; }

    public SubjectModel? Subject { get; set; }
    
    [Required]
    public int MatrixId { get; set; }
    
    [ForeignKey("AvaliableTime")]
    public int AvaliableTimeId { get; set; }

    public AvaliableTimeModel? AvaliableTime { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
  }
}
