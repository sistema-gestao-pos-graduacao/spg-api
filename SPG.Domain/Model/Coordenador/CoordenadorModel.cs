using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model.Coordenador
{
  public class CoordenadorModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Person")]
    public int PersonId { get; set; }

    public PersonModel? Person { get; set; }
    
  }
}
