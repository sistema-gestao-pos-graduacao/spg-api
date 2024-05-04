namespace SPG.Domain.Dto
{
  public class SystemParamsDto
  {
    public required string Id { get; set; }

    public int? Integer { get; set; }

    public string? String { get; set; }

    public bool? Boolean { get; set; }

    public double? Double { get; set; }
  }
}
