using SPG.Intf.Data;
using SPG.Intf.Model;

namespace SPG.Intf.Interfaces
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
