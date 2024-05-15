using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface IClassRepository
    {
        IEnumerable<ClassModel> GetAll();
        ClassModel GetById(int id);
        ClassModel? GetByName(string name);
        void Add(ClassModel curriculum);
        void Update(ClassModel curriculum);
        void Delete(int id);
    }
}
