﻿namespace SPG.Domain.Dto
{
  public class ClassScheduleDto
  {
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherName {  get; set; } = string.Empty;
    public int? TeacherId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string Color { get; set; } = string.Empty;
    public IList<int> RelatedClassesIds { get; set; } = [];
  }
}
