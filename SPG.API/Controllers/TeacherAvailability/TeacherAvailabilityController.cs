using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.TeacherAvailability
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class TeacherAvailabilitysController(ITeacherAvailabilityService service) : ControllerBase
  {
    private readonly ITeacherAvailabilityService _service = service;

    [HttpGet]
    public IActionResult Get()
    {
      var subjects = _service.GetAllTeacherAvailabilities();
      return Ok(subjects);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var teacherAvailability = _service.GetTeacherAvailabilityById(id);
      return Ok(teacherAvailability);
    }

    [HttpPost]
    public IActionResult Post([FromBody] TeacherAvailabilityDto teacherAvailability)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddTeacherAvailability(teacherAvailability);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, teacherAvailability);
    }

    [HttpPut]
    public IActionResult Put([FromBody] TeacherAvailabilityDto teacherAvailability)
    {
      _service.UpdateTeacherAvailability(teacherAvailability);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var existingTeacherAvailability = _service.GetTeacherAvailabilityById(id);
      if (existingTeacherAvailability == null)
      {
        return NotFound();
      }
      _service.DeleteTeacherAvailability(id);
      return NoContent();
    }
  }
}
