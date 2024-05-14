namespace SPG.Domain.Dto
{
  public class ClassDto
  {
    public int Id { get; set; }

    public int CurriculumId { get; set; }

    public string CurriculumName { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<string> Students { get; set; } = [];
  }
}
