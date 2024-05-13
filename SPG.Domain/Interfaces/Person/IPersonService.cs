using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDto> GetAllPersons();
        PersonDto GetPersonById(int id);
        PersonDto GetPersonByUserId(string userId);
        Task<PersonDto> AddPerson(PersonDto dto);
        PersonDto UpdatePerson(PersonDto person);
        Task DeletePerson(int id);
    }
}
