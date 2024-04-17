using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPG.Domain.Model.Matrix;

namespace SPG.Domain.Model.Teacher
{
  public class TeacherModel
  {
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Person")]
    public int PersonId { get; set; }

    public PersonModel? Person { get; set; }
    
    // Campo licences Many to many com licence
  }
}
