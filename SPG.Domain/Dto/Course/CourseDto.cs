namespace SPG.Domain.Dto
{
  public class CourseDto
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? CoordinatorId { get; set; }
    public string? Coordinator { get; set; }
  }
}

