﻿namespace SPG.Domain.Dto
{
  public class UserDto
  {
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
  }

  public class ForgotPasswordDto
  {
    public required string Email { get; set; }
  }
}