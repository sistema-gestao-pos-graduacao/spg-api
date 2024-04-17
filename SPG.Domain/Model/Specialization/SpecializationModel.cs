using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class SpecializationModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Coordenador")]
    public int CoordenadorId { get; set; } 

    public CoordinatorModel? Coordenador { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
  }
}
