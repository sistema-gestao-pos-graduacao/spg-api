using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class ClassScheduleModel
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Subject")]
    public int SubjectId { get; set; }

    public SubjectModel? Subject { get; set; }

    [Required]
    public DateTime StartDateTime { get; set; }

    [Required]
    public DateTime EndDateTime { get; set; }

    [StringLength(7)]
    public string Color { get; set; } = string.Empty;

    public IList<int> RelatedClassesIds { get; set; } = [];
  }
}
