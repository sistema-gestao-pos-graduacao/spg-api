using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<PersonModel> GetAll();
        PersonModel GetById(int id);
        void Add(PersonModel person);
        void Update(PersonModel person);
        void Delete(int id);
    }
}
