using Microsoft.AspNetCore.Mvc;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Login
{
  [ApiController]
  [Route("api/oAuth")]
  public class LoginController(IoAuthService service) : ControllerBase
  {
    private readonly IoAuthService _service = service;
    // POST: api/oAuth
    [HttpPost]
    public IActionResult Post([FromBody] PersonDto person)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddPerson(person);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, person);
    }
  }
}
