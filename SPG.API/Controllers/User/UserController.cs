using Microsoft.AspNetCore.Mvc;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.User
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        // GET: api/users
        [HttpGet]
        public IActionResult Get()
        {
            var users = _service.GetAllUsers();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _service.GetUserById(id);
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult Post([FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.AddUser(user);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, user);
        }

        // PUT: api/users/5
        [HttpPut]
        public IActionResult Put([FromBody] UserDto user)
        {
            _service.UpdateUser(user);

            return NoContent();
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingUser = _service.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            _service.DeleteUser(id);
            return NoContent();
        }
    }
}
