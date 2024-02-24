namespace SPG.Domain.Dto
{
  public class LoginDto
  {
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string Token { get; set; } = string.Empty;
  }
}
