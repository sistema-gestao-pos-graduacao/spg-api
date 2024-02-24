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
      var users = _repository.GetAll().ToList();
      if (!users.Any())
        return new List<UserDto>();

      var usersDto = new List<UserDto>();

      users.ForEach(user => { usersDto.Add(_mapper.Map<UserDto>(user)); });

      return usersDto;
    } 

    public UserDto GetUserById(int id)
    {
      return _mapper.Map<UserDto>(_repository.GetById(id));
    }

    public UserModel GetUserByLogin(int id)
    {
      return _repository.GetById(id);
    }

    public UserDto AddUser(UserDto dto)
    {
      var user = _mapper.Map<UserModel>(dto);

      _repository.Add(user);

      return _mapper.Map<UserDto>(user);
    }

    public UserDto UpdateUser(UserDto user)
    {
      _repository.Update(_mapper.Map<UserModel>(user));

      return user;
    }

    public void DeleteUser(int id)
    {
      _repository.Delete(id);
    }
  }
}
