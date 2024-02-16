using SPG.Domain.Dto.Person;
using SPG.Domain.Model.Person;

namespace SPG.Domain.Interfaces.Person
{
    public interface IPersonService
    {
        IEnumerable<PersonModel> GetAllPersons();
        PersonDto GetPersonById(int id);
        PersonModel AddPerson(PersonDto person);
        void UpdatePerson(int id, PersonDto person);
        void DeletePerson(int id);
    }
}
