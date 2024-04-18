using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class ExceptionDateModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("AvailableTime")]
    public int AvailableTimeId { get; set; }

    public AvailableTimeModel? AvailableTime { get; set; }
    
    [Required]
    [StringLength(200)]
    public DateOnly Date { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
    
  }
}
