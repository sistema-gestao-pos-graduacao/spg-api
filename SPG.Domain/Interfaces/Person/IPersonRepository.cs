using SPG.Domain.Model.Person;

namespace SPG.Domain.Interfaces.Person
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
