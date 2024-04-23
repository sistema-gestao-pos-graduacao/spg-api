using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        IEnumerable<SubjectModel> GetAll();
        SubjectModel GetById(int id);
        void Add(SubjectModel subject);
        void Update(SubjectModel subject);
        void Delete(int id);
    }
}
