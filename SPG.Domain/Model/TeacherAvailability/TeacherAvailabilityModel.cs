using System.ComponentModel.DataAnnotations;

namespace SPG.Domain.Model
{
  public class TeacherAvailabilityModel
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime StartDateTime { get; set; }

    [Required]
    public DateTime EndDateTime { get; set; }
  }
}
