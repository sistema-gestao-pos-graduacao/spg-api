using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Controllers.Base;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Class
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class ClassesController(IClassService service) : SPGBaseController<ClassDto>
  {
    private readonly IClassService _service = service;

    [HttpGet]
    public IActionResult GetAll([FromQuery] Dictionary<string, string> filters)
    {
      var classDtoes = _service.GetAllClasses();
      return Ok(ApplyFilters(classDtoes, filters));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var classDto = _service.GetClassById(id);
      return Ok(classDto);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ClassDto classDto)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddClass(classDto);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut]
    public IActionResult Put([FromBody] ClassDto classDto)
    {
      _service.UpdateClass(classDto);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var existingClass = _service.GetClassById(id);
      if (existingClass == null)
      {
        return NotFound();
      }
      _service.DeleteClass(id);
      return NoContent();
    }
  }
}
