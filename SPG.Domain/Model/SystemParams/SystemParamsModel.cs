using System.ComponentModel.DataAnnotations;

namespace SPG.Domain.Model
{
  public class SystemParamsModel
  {
    [Key]
    [StringLength(255)]
    public required string Id { get; set; }

    public int? Integer { get; set; }

    [StringLength(4000)]
    public string? String { get; set; }

    public bool? Boolean { get; set; }

    public double? Double { get; set; }
  }
}
