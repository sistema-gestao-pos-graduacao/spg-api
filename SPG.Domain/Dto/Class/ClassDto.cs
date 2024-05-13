namespace SPG.Domain.Dto
{
  public class ClassDto
  {
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public string SubjectName { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int? Building { get; set; }

    public int? Room { get; set; }

    public List<string> Students { get; set; } = [];
  }
}
