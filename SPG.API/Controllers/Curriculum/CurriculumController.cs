using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Controllers.Base;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Curriculum
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class CurriculumsController(ICurriculumService service) : SPGBaseController<CurriculumDto>
  {
    private readonly ICurriculumService _service = service;

    [HttpGet]
    public IActionResult GetAll([FromQuery] Dictionary<string, string> filters)
    {
      var curriculums = _service.GetAllCurriculums();
      return Ok(ApplyFilters(curriculums, filters));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var curriculum = _service.GetCurriculumById(id);
      return Ok(curriculum);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CurriculumDto curriculum)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = _service.AddCurriculum(curriculum);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut]
    public IActionResult Put([FromBody] CurriculumDto curriculum)
    {
      _service.UpdateCurriculum(curriculum);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var existingCurriculum = _service.GetCurriculumById(id);
      if (existingCurriculum == null)
      {
        return NotFound();
      }
      _service.DeleteCurriculum(id);
      return NoContent();
    }
  }
}
