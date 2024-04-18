using SPG.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class PersonModel()
  {
    [Key]
    public int Id { get; set; }

    [StringLength(11)]
    public string Cpf { get; set; } = string.Empty;

    [ForeignKey("User")]
    public string UserId { get; set; } = string.Empty;

    public UserModel? User { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(256)]
    [Required]
    public string Email { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }

    [Required]
    public PersonTypeEnum PersonType { get; set; }
  }
}
