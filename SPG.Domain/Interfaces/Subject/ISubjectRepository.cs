using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        IEnumerable<SubjectModel> GetAll();
        SubjectModel GetById(int id);
        void Add(SubjectModel person);
        void Update(SubjectModel person);
        void Delete(int id);
    }
}
