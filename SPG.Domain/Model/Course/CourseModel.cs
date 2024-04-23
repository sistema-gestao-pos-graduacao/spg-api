using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class CourseModel()
  {
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [ForeignKey("Coordinator")]
    public int? CoordinatorId { get; set; }

    public PersonModel? Coordinator { get; set; }

  }
}
