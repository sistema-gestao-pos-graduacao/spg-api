using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Properties;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using System.Security.Claims;

namespace SPG.API.Controllers.Login
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class ContextController(UserManager<UserModel> userManager, IPersonService personService) : ControllerBase
  {
    private readonly UserManager<UserModel> _userManager = userManager;
    private readonly IPersonService _personService = personService;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      if (string.IsNullOrEmpty(userId))
        return NotFound(Resources.UserNotFound);

      var user = await _userManager.FindByIdAsync(userId);
      if (user == null)
        return NotFound(Resources.UserNotFound);

      var person = _personService.GetPersonByUserId(userId);

      var roles = await _userManager.GetRolesAsync(user);

      return Ok(new ContextDto
      {
        UserId = user.Id,
        PersonId = person.Id,
        Username = user.UserName ?? "",
        Email = user.Email ?? "",
        Roles = roles
      });
    }
  }
}
