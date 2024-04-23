using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface ICurriculumRepository
    {
        IEnumerable<CurriculumModel> GetAll();
        CurriculumModel GetById(int id);
        void Add(CurriculumModel curriculum);
        void Update(CurriculumModel curriculum);
        void Delete(int id);
    }
}
