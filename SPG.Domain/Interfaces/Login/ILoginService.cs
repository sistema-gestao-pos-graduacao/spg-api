using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
  public interface ILoginService
  {
    void AuthenticateUser(LoginDto loginDto);
  }
}
