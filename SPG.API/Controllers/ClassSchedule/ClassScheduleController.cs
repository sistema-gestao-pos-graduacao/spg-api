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
      var classSchedules = _service.GetAllClassSchedules();
      return Ok(ApplyFilters(classSchedules, filters));
    }

    [HttpGet]
    [Route("FilteredByClass/{id}")]
    public IActionResult GetAllFilteredByClass([FromQuery] Dictionary<string, string> filters, int id)
    {
      var classSchedules = _service.GetAllFilteredByClass(id);
      return Ok(ApplyFilters(classSchedules, filters));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var classSchedule = _service.GetClassScheduleById(id);
      return Ok(classSchedule);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ClassScheduleDto classSchedule)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddClassSchedule(classSchedule);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPost]
    [Route("SaveAll")]
    public IActionResult PostAll([FromBody] List<ClassScheduleDto> classSchedules)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var results = _service.AddClassSchedules(classSchedules);

      return Ok(results);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ClassScheduleDto classSchedule)
    {
      _service.UpdateClassSchedule(classSchedule);

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

    [HttpPost]
    [Route("DeleteAll")]
    public IActionResult DeleteAll([FromBody] List<int> ids)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      _service.DeleteClassSchedules(ids);

      return NoContent();
    }
  }
}
