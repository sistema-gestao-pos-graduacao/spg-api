using System.ComponentModel.DataAnnotations;

namespace SPG.Domain.Model
{
  public class StudentModel()
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
