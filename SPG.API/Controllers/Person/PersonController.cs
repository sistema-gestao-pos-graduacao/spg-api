using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Controllers.Base;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Person
{
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class PersonsController(IPersonService service) : SPGBaseController<PersonDto>
  {
    private readonly IPersonService _service = service;

    [HttpGet]
    public IActionResult GetAll([FromQuery] Dictionary<string, string> filters)
    {
      var persons = _service.GetAllPersons();

      return Ok(ApplyFilters(persons, filters));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var person = _service.GetPersonById(id);
      return Ok(person);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PersonDto person)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var result = await _service.AddPerson(person);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut]
    public IActionResult Put([FromBody] PersonDto person)
    {
      _service.UpdatePerson(person);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var existingPerson = _service.GetPersonById(id);
      if (existingPerson == null)
      {
          return NotFound();
      }
      _service.DeletePerson(id);
      return NoContent();
    }
  }
}
