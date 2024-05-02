using SPG.Domain.Enums;

namespace SPG.Domain.Dto
{
    public class SubjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CurriculumId { get; set; }

        public int CurriculumName { get; set; }

        public int? TeacherId { get; set; }

        public string TeacherName { get; set; } = string.Empty;

        public int Hours { get; set; }

        public string Location { get; set; } = string.Empty;

        public int? Building { get; set; }

        public int? Room { get; set; }

        public string Considerations = string.Empty;

        public List<string> Students { get; set; } = [];

        public WeekDaysEnum WeekDay { get; set; }

        public string Syllabus { get; set; } = string.Empty;
    }
}

