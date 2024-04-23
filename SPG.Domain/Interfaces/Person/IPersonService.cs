using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDto> GetAllPersons();
        PersonDto GetPersonById(int id);
        Task<PersonDto> AddPerson(PersonDto dto);
        PersonDto UpdatePerson(PersonDto person);
        void DeletePerson(int id);
    }
}
