using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(int id);
        UserDto AddUser(UserDto person);
        UserDto UpdateUser(UserDto person);
        void DeleteUser(int id);
    }
}
