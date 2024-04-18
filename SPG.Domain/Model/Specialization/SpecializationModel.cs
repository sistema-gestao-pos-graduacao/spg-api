using System.ComponentModel.DataAnnotations;

namespace SPG.Domain.Model
{
  public class SpecializationModel()
  {
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
  }
}
