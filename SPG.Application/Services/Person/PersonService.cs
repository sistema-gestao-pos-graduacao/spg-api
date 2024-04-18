using AutoMapper;
using SPG.Application.Properties;
using SPG.Domain.Dto;
using SPG.Domain.Enums;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Domain.Utils;

namespace SPG.Application.Services
{
  public class PersonService(IPersonRepository repository, IUserService userService, IMapper mapper) : IPersonService
  {
    private readonly IMapper _mapper = mapper;
    private readonly IPersonRepository _repository = repository;
    private readonly IUserService _userService = userService;

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

    public async Task<PersonDto> AddPerson(PersonDto dto)
    {
      if(dto.PersonType == null)
        throw new Exception(Resources.InvalidUserRoleName);

      var roleName = Enum.GetName(typeof(PersonTypeEnum), dto.PersonType);

      if (string.IsNullOrEmpty(roleName))
        throw new Exception(Resources.InvalidUserRoleName);

      var person = _mapper.Map<PersonModel>(dto);
      person.Cpf = CPFUtils.RemoveSpecialCharacters(person.Cpf);

      person.UserId = await _userService.GenerateNewUser(person.Name, person.Email, roleName);

      try
      {
        _repository.Add(person);
      }
      catch
      {
        if(!string.IsNullOrEmpty(person.UserId))
          await _userService.DeleteUserAsync(person.UserId);
      }
      

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
