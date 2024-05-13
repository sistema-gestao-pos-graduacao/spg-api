using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Controllers.Base;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Subject
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class SubjectsController(ISubjectService service) : SPGBaseController<SubjectDto>
  {
    private readonly ISubjectService _service = service;

    [HttpGet]
    public IActionResult GetAll([FromQuery] Dictionary<string, string> filters)
    {
      var subjects = _service.GetAllSubjects();
      return Ok(ApplyFilters(subjects, filters));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var subject = _service.GetSubjectById(id);
      return Ok(subject);
    }

    [HttpPost]
    public IActionResult Post([FromBody] SubjectDto subject)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddSubject(subject);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPost]
    [Route("SaveAll")]
    public IActionResult PostAll([FromBody] List<SubjectDto> subjects)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var results = _service.AddSubjects(subjects);

      return Ok(results);
    }

    [HttpPut]
    public IActionResult Put([FromBody] SubjectDto subject)
    {
      _service.UpdateSubject(subject);

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

    [HttpPost]
    [Route("DeleteAll")]
    public IActionResult DeleteAll([FromBody] List<int> ids)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      _service.DeleteSubjects(ids);

      return NoContent();
    }
  }
}
