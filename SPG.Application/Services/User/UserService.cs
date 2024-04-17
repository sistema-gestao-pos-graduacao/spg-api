using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPG.Domain.Model;
using SPG.Application.Properties;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace SPG.Application.Services
{
  public class UserService(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration) : IUserService
  {
    private readonly UserManager<UserModel> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IMapper _mapper = mapper;
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Retorna todos os usuários do sistema
    /// </summary>
    /// <returns>Retorna uma lista de usuários </returns>
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
      var users = await _userManager.Users.ToListAsync();
      return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    /// <summary>
    /// Retorna um usuário pelo id
    /// </summary>
    /// <param name="id">Id do usuário</param>
    /// <returns>Retorna o usuário correspondente</returns>
    public async Task<UserDto> GetUserByIdAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      return _mapper.Map<UserDto>(user);
    }

    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="userDto">Usuário</param>
    /// <returns>Retorna o usuário correspondente</returns>
    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
      var user = _mapper.Map<UserModel>(userDto);
      if (user.UserName == null)
        throw new Exception(Resources.ValidUserException);

      user.Id = Guid.NewGuid().ToString();
      user.SecurityStamp = Guid.NewGuid().ToString();

      var result = await _userManager.CreateAsync(user, userDto.Password);

      if (!result.Succeeded)
        throw new Exception(result.Errors.First().ToString());

      await PutUserRole(user.UserName, userDto.Role);
      return _mapper.Map<UserDto>(user);
    }

    /// <summary>
    /// Edita um usuário
    /// </summary>
    /// <param name="userDto">Usuário</param>
    public async Task UpdateUserAsync(UserDto userDto)
    {
      var user = await _userManager.FindByIdAsync(userDto.Id);
      await _userManager.UpdateAsync(_mapper.Map<UserModel>(user));
    }

    /// <summary>
    /// Exclui um usuário pelo id
    /// </summary>
    /// <param name="id">Id do usuário</param>
    public async Task DeleteUserAsync(string id)
    {
      var user = await _userManager.FindByIdAsync(id);
      if (user != null)
        await _userManager.DeleteAsync(user);
    }

    /// <summary>
    /// Inlcui a role do usuario
    /// </summary>
    public async Task PutUserRole(string nomeDoUsuario, string nomeDaRole)
    {
      var usuario = await _userManager.FindByNameAsync(nomeDoUsuario);
      var role = await _roleManager.FindByNameAsync(nomeDaRole);

      if (usuario != null && role != null)
      {
        await _userManager.AddToRoleAsync(usuario, nomeDaRole);
      }
    }

    public async Task ForgotPassword(ForgotPasswordDto model)
    {
      // Find the user by email address
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null || string.IsNullOrEmpty(user.Email) || !(await _userManager.IsEmailConfirmedAsync(user)))
        return;

      var token = await _userManager.GeneratePasswordResetTokenAsync(user);

      await SendEmailAsync(
        user.Email,
        "Password Reset",
        @$"Please reset your password by clicking this link: 
           <a href='www.url.com/{token}'>Resetar Senha</a>"
        );

      return;
    }

    private async Task SendEmailAsync(string email, string subject, string message)
    {
      var smtpCredentialsSection = _configuration.GetSection("SmtpCredentials");

      var smtpClient = new SmtpClient(smtpCredentialsSection["ServerAddress"])
      {
        Port = 587,
        Credentials = new NetworkCredential(smtpCredentialsSection["Username"], smtpCredentialsSection["Password"]),
        EnableSsl = true,
      };

      var mailMessage = new MailMessage
      {
        From = new MailAddress(smtpCredentialsSection["FromEmail"] ?? ""),
        Subject = subject,
        Body = message,
        IsBodyHtml = true,
      };

      mailMessage.To.Add(email);

      await smtpClient.SendMailAsync(mailMessage);
    }
  }
}
