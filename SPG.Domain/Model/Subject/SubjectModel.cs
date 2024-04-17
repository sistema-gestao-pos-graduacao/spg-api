using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class SubjectModel
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey("Matrix")]
    public int MatrixId { get; set; }

    public MatrixModel? Matrix { get; set; }
    
    [Required]
    public int Hours { get; set; }
    
    // Campo necessary_licences Many to many com licence
  }
}
