using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Controllers.Base;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.ClassSchedule
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class ClassScheduleController(IClassScheduleService service) : SPGBaseController<ClassScheduleDto>
  {
    private readonly IClassScheduleService _service = service;

    [HttpGet]
    public IActionResult GetAll([FromQuery] Dictionary<string, string> filters)
    {
      var subjects = _service.GetAllClassSchedules();
      return Ok(ApplyFilters(subjects, filters));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var subject = _service.GetClassScheduleById(id);
      return Ok(subject);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ClassScheduleDto subject)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddClassSchedule(subject);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPost]
    [Route("SaveAll")]
    public IActionResult PostAll([FromBody] List<ClassScheduleDto> subjects)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var results = _service.AddClassSchedules(subjects);

      return Ok(results);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ClassScheduleDto subject)
    {
      _service.UpdateClassSchedule(subject);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var existingClassSchedule = _service.GetClassScheduleById(id);
      if (existingClassSchedule == null)
      {
        return NotFound();
      }
      _service.DeleteClassSchedule(id);
      return NoContent();
    }
  }
}
