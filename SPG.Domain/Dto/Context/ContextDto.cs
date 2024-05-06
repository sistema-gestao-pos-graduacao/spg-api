namespace SPG.Domain.Dto
{
  public class ContextDto
  {
    /// <summary>
    /// Id do usuario
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    /// Id de pessoa
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Nome de usuário.
    /// </summary>
    public string Username { get; set; } = "";

    /// <summary>
    /// Email do usuário.
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Lista de roles do usuario.
    /// </summary>
    public IList<string>? Roles { get; set; }
  }
}
