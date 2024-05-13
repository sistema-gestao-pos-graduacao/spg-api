using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        IEnumerable<SubjectModel> GetAll();
        SubjectModel GetById(int id);
        void Add(SubjectModel subject);
        void AddAll(IList<SubjectModel> subjects);
        void Update(SubjectModel subject);
        void Delete(int id);
      void DeleteAll(List<int> ids);
    }
}
