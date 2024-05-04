using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface ISubjectService
    {
        IEnumerable<SubjectDto> GetAllSubjects();
        SubjectDto GetSubjectById(int id);
        SubjectDto AddSubject(SubjectDto subject);
        SubjectDto UpdateSubject(SubjectDto subject);
        IList<SubjectDto> AddSubjects(List<SubjectDto> subjects);
        void DeleteSubject(int id);
    }
}
