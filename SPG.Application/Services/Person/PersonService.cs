using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;

namespace SPG.Application.Services
{
  public class PersonService(IPersonRepository repository, IMapper mapper) : IPersonService
  {
    private readonly IMapper _mapper = mapper;
    private readonly IPersonRepository _repository = repository;

    public IEnumerable<PersonDto> GetAllPersons()
    {
      var persons = _repository.GetAll().ToList();
      if (!persons.Any())
        return new List<PersonDto>();

      var personsDto = new List<PersonDto>();

      persons.ForEach(person => { personsDto.Add(_mapper.Map<PersonDto>(person)); });

      return personsDto;
    }

    public PersonDto GetPersonById(int id)
    {
      return _mapper.Map<PersonDto>(_repository.GetById(id));
    }

    public PersonDto AddPerson(PersonDto dto)
    {
      var person = _mapper.Map<PersonModel>(dto);

      _repository.Add(person);

      return _mapper.Map<PersonDto>(person);
    }

    public PersonDto UpdatePerson(PersonDto person)
    {
      _repository.Update(_mapper.Map<PersonModel>(person));

      return person;
    }

    public void DeletePerson(int id)
    {
      _repository.Delete(id);
    }
  }

}
