using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Properties;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Login
{
  [Route("api")]
  [ApiController]
  [AllowAnonymous]
  public class LoginController(ILoginService loginService) : ControllerBase
  {
    private readonly ILoginService _loginService = loginService;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
      var result = await _loginService.AuthenticateUser(login);
      if (result.Succeeded)
        return Ok(Resources.SuccessfulLogin);
      else
        return Unauthorized(Resources.InvalidLogin);
    }

    [HttpPost("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

      return Ok(Resources.SuccessfulLogout);
    }
  }
}
