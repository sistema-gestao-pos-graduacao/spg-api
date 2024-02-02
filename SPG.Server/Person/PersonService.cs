using AutoMapper;
using SPG.Intf.Dto;
using SPG.Intf.Interfaces;
using SPG.Intf.Model;

namespace SPG.Server.Services
{
    public class PersonService(IPersonRepository repository, IMapper mapper) : IPersonService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IPersonRepository _repository = repository;

        public IEnumerable<PersonDto> GetAllPersons()
        {
            return _mapper.Map<IEnumerable<PersonDto>>(_repository.GetAll());
        }

        public PersonDto GetPersonById(int id)
        {
            return _mapper.Map<PersonDto>(_repository.GetById(id));
        }

        public void AddPerson(PersonDto person)
        {
            _repository.Add(_mapper.Map<PersonModel>(person));
        }

        public void UpdatePerson(PersonDto person)
        {
            var existingPerson = _repository.GetById(person.Id);
            if (existingPerson == null)
            {
                throw new ArgumentException("Person not found");
            }


            _repository.Update(_mapper.Map<PersonModel>(person));
        }

        public void DeletePerson(int id)
        {
            _repository.Delete(id);
        }
    }

}
