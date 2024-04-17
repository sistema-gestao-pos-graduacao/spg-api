using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class SpecializationModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Coordinator")]
    public int CoordinatorId { get; set; } 

    public CoordinatorModel? Coordinator { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
  }
}
