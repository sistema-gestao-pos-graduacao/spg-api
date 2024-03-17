namespace SPG.Domain.Dto
{
  public class TeacherAvailabilityDto
  {
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
  }
}
