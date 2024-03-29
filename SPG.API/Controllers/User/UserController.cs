﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;


namespace SPG.API.Controllers.Users
{
  [EnableCors("MyPolicy")]
  [Authorize(Roles = "Admin")]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
      var users = await _userService.GetAllUsersAsync();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(string id)
    {
      var user = await _userService.GetUserByIdAsync(id);
      if (user == null)
      {
        return NotFound();
      }
      return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(UserDto userDto)
    {
      try
      {
        var createdUser = await _userService.CreateUserAsync(userDto);
        return CreatedAtAction("Create", createdUser);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserDto userDto)
    {
      await _userService.UpdateUserAsync(userDto);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
      await _userService.DeleteUserAsync(id);
      return NoContent();
    }
  }
}
