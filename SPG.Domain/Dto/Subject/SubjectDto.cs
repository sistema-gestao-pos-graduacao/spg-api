using SPG.Domain.Enums;

namespace SPG.Domain.Dto
{
    public class SubjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int CurriculumId { get; set; }

        public string CurriculumName { get; set; } = string.Empty;

        public int? TeacherId { get; set; }

        public string TeacherName { get; set; } = string.Empty;

        public int NumberOfClasses { get; set; }

        public string Location { get; set; } = string.Empty;

        public int? Building { get; set; }

        public int? Room { get; set; }

        public string Considerations { get; set; } = string.Empty;

        public List<string> Students { get; set; } = [];

        public WeekDaysEnum WeekDay { get; set; }

        public string Syllabus { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;
     }
}

