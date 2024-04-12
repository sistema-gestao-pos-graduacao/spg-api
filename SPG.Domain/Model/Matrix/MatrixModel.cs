using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPG.Domain.Model.Specialization;

namespace SPG.Domain.Model.Matrix
{
  public class MatrixModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Specialization")]
    public int SpecializationId { get; set; } 

    public SpecializationModel? Specialization { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
  }
}
