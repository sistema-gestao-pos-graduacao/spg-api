using SPG.Intf.Dto;

namespace SPG.Intf.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDto> GetAllPersons();
        PersonDto GetPersonById(int id);
        void AddPerson(PersonDto person);
        void UpdatePerson(PersonDto person);
        void DeletePerson(int id);
    }
}
