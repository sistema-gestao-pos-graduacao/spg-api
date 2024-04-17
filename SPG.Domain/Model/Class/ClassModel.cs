using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPG.Domain.Model.Matrix;

namespace SPG.Domain.Model.Class
{
  public class ClassModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Matrix")]
    public int MatrixId { get; set; }

    public MatrixModel? Matrix { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
  }
}
