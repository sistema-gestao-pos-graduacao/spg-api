using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
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

    public AvailableTimeModel? AvaliableTime { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
  }
}
