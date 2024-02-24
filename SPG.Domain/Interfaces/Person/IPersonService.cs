using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDto> GetAllPersons();
        PersonDto GetPersonById(int id);
        PersonDto AddPerson(PersonDto person);
        PersonDto UpdatePerson(PersonDto person);
        void DeletePerson(int id);
    }
}
