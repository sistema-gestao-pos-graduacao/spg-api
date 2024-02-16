using SPG.Domain.Dto.Person;
using SPG.Domain.Model.Person;

namespace SPG.Domain.Interfaces.Person
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
