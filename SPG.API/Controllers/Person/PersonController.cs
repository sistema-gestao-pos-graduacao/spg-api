﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.Person
{
  [EnableCors("MyPolicy")]
  [ApiController]
  [Route("api/[controller]")]
  [Authorize]
  public class PersonsController(IPersonService service) : ControllerBase
  {
    private readonly IPersonService _service = service;

    [HttpGet]
    public IActionResult Get()
    {
      var persons = _service.GetAllPersons();
      return Ok(persons);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var person = _service.GetPersonById(id);
      return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] PersonDto person)
    {
      if (!ModelState.IsValid)
          return BadRequest(ModelState);

      var result = _service.AddPerson(person);

      return CreatedAtAction(nameof(Get), new { id = result.Id }, person);
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
