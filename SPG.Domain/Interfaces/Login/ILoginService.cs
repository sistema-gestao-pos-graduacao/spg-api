using Microsoft.AspNetCore.Identity;
using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
  public interface ILoginService
  {
    Task<SignInResult> AuthenticateUser(LoginDto loginDto);
  }
}
