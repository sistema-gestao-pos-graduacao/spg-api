using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPG.Domain.Model.Coordenador;

namespace SPG.Domain.Model.Studant
{
  public class StudantModel()
  {
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int RegisterCode { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    //Many to many com classes
    
  }
}
