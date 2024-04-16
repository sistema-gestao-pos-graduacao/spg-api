using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Properties;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Login
{
  [Route("api/login")]
  [ApiController]
  [AllowAnonymous]
  public class LoginController(ILoginService loginService) : ControllerBase
  {
    private readonly ILoginService _loginService = loginService;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
      var result = await _loginService.AuthenticateUser(login);
      if (result.Succeeded)
        return Ok(Resources.SuccessfulLogin);
      else
        return Unauthorized(Resources.InvalidLogin);
    }
  }
}
