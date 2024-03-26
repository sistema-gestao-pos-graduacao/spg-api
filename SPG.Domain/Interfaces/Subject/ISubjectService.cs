using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface ISubjectService
    {
        IEnumerable<SubjectDto> GetAllSubjects();
        SubjectDto GetSubjectById(int id);
        SubjectDto AddSubject(SubjectDto person);
        SubjectDto UpdateSubject(SubjectDto person);
        void DeleteSubject(int id);
    }
}
