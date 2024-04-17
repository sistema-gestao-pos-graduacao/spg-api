using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG.Domain.Model
{
  public class ExceptionDatesModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("AvaliableTime")]
    public int AvaliableTimeId { get; set; }

    public AvailableTimeModel? AvaliableTime { get; set; }
    
    [Required]
    [StringLength(200)]
    public DateOnly Date { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
    
  }
}
