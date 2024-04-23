using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
  public interface IUserService
  {
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(string id);
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task UpdateUserAsync(UserDto userDto);
    Task DeleteUserAsync(string id);
    Task ForgotPassword(ForgotPasswordDto model);
    Task<string> GenerateNewUser(string personName, string personEmail, string roleName);
  }
}



