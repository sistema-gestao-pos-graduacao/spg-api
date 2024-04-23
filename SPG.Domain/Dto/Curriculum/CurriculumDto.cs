namespace SPG.Domain.Dto
{
  public class CurriculumDto
  {
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CourseId { get; set; }

    public string Course { get; set; } = string.Empty;
  }
}
