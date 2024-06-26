﻿namespace SPG.Domain.Dto
{
  public class TeacherAvailabilityDto
  {
    public int Id { get; set; }
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Color { get; set; } = string.Empty;
  }
}
