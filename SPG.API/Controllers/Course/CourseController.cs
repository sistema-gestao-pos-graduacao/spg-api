using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Controllers.Base;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Course
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class CoursesController(ICourseService service) : SPGBaseController<CourseDto>
  {
    private readonly ICourseService _service = service;

    [HttpGet]
    public IActionResult GetAll([FromQuery] Dictionary<string, string> filters)
    {
      var subjects = _service.GetAllCourses();
      return Ok(ApplyFilters(subjects, filters));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var course = _service.GetCourseById(id);
      return Ok(course);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CourseDto course)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddCourse(course);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut]
    public IActionResult Put([FromBody] CourseDto course)
    {
      _service.UpdateCourse(course);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var existingCourse = _service.GetCourseById(id);
      if (existingCourse == null)
      {
        return NotFound();
      }
      _service.DeleteCourse(id);
      return NoContent();
    }
  }
}
