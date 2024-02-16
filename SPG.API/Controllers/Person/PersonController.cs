using Microsoft.AspNetCore.Mvc;
using SPG.Domain.Dto.Person;
using SPG.Domain.Interfaces.Person;

namespace SPG.API.Controllers.Person
{

    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController(IPersonService service) : ControllerBase
    {
        private readonly IPersonService _service = service;

        // GET: api/persons
        [HttpGet]
        public IActionResult Get()
        {
            var persons = _service.GetAllPersons();
            return Ok(persons);
        }

        // GET: api/persons/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _service.GetPersonById(id);
            return Ok(person);
        }

        // POST: api/persons
        [HttpPost]
        public IActionResult Post([FromBody] PersonDto person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.AddPerson(person);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, person);
        }

        // PUT: api/persons/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonDto person)
        {
            _service.UpdatePerson(id, person);

            return NoContent();
        }

        // DELETE: api/persons/5
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
