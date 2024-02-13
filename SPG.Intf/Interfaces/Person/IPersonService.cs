using SPG.Intf.Dto;
using SPG.Intf.Model;

namespace SPG.Intf.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonModel> GetAllPersons();
        PersonDto GetPersonById(int id);
        void AddPerson(PersonDto person);
        void UpdatePerson(PersonDto person);
        void DeletePerson(int id);
    }
}
