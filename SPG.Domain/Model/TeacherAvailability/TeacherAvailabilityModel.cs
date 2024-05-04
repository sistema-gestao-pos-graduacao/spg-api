using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class TeacherAvailabilityModel
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("Teacher")]
    public int TeacherId { get; set; }

    public PersonModel? Teacher { get; set; }

    [Required]
    public DateTime StartDateTime { get; set; }

    [Required]
    public DateTime EndDateTime { get; set; }

    [StringLength(7)]
    public string Color { get; set; } = string.Empty;
  }
}
