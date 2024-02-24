using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;

namespace SPG.Application.User
{
    public class UserService(IUserRepository repository, IMapper mapper) : IUserService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _repository = repository;

        public IEnumerable<UserDto> GetAllUsers()
        {
            var persons = _repository.GetAll().ToList();
            if (!persons.Any())
                return new List<UserDto>();

            var personsDto = new List<UserDto>();

            persons.ForEach(person => { personsDto.Add(_mapper.Map<UserDto>(person)); });

            return personsDto;
        }

        public UserDto GetUserById(int id)
        {
            return _mapper.Map<UserDto>(_repository.GetById(id));
        }

        public UserDto AddUser(UserDto dto)
        {
            var person = _mapper.Map<UserModel>(dto);

            _repository.Add(person);

            return _mapper.Map<UserDto>(person);
        }

        public UserDto UpdateUser(UserDto person)
        {
            _repository.Update(_mapper.Map<UserModel>(person));

            return person;
        }

        public void DeleteUser(int id)
        {
            _repository.Delete(id);
        }
    }

}
