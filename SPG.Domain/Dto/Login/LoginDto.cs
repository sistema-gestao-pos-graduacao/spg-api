namespace SPG.Domain.Dto
{
  public class LoginDto
  {
    /// <summary>
    /// Nome de usuário.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Senha codificada em Base64.
    /// </summary>
    public required string Password { get; set; }
  }
}
