using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SPG.Domain.Model.AvaliableTime;

namespace SPG.Domain.Model.ExceptionDates
{
  public class ExceptionDatesModel()
  {
    [Key]
    public int Id { get; set; }

    [ForeignKey("AvaliableTime")]
    public int AvaliableTimeId { get; set; }

    public AvaliableTimeModel? AvaliableTime { get; set; }
    
    [Required]
    [StringLength(200)]
    public DateOnly Date { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;
    
  }
}
