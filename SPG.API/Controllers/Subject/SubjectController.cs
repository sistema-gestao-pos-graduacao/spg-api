using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Subject
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class SubjectsController(ISubjectService service) : ControllerBase
  {
    private readonly ISubjectService _service = service;

    [HttpGet]
    public IActionResult Get()
    {
      var subjects = _service.GetAllSubjects();
      return Ok(subjects);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var person = _service.GetSubjectById(id);
      return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] SubjectDto person)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddSubject(person);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, person);
    }

    [HttpPut]
    public IActionResult Put([FromBody] SubjectDto person)
    {
      _service.UpdateSubject(person);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var existingSubject = _service.GetSubjectById(id);
      if (existingSubject == null)
      {
        return NotFound();
      }
      _service.DeleteSubject(id);
      return NoContent();
    }
  }
}
