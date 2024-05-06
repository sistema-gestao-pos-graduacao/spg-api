namespace SPG.Domain.Dto
{
  public class ClassScheduleDto
  {
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Color { get; set; } = string.Empty;
  }
}
