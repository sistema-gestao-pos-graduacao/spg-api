﻿using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPG.Domain.Model;
using SPG.Application.Properties;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace SPG.Application.Services
{
  public class UserService(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IEmailService emailService, IConfiguration configuration) : IUserService
  {
    private readonly UserManager<UserModel> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IMapper _mapper = mapper;
    private readonly IEmailService _emailService = emailService;
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
    public async Task PutUserRole(string nomeDoUsuario, string roleName)
    {
      var usuario = await _userManager.FindByNameAsync(nomeDoUsuario);
      var role = await _roleManager.FindByNameAsync(roleName);

      if (usuario != null && role != null)
      {
        await _userManager.AddToRoleAsync(usuario, roleName);
      }
      else
        throw new Exception(Resources.InvalidUserRoleName);
    }

    /// <summary>
    /// Gera token para recuperar senha
    /// </summary>
    public async Task ForgotPassword(ForgotPasswordDto model)
    {
      // Find the user by email address
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null || string.IsNullOrEmpty(user.Email))
        return;

      var token = await _userManager.GeneratePasswordResetTokenAsync(user);

      await _emailService.SendEmailAsync(
        user.Email,
        "Password Reset",
        @$"Please reset your password by clicking this link: 
           <a href='www.url.com/{token}'>Resetar Senha</a>"
        );

      return;
    }

    /// <summary>
    /// Gera um novo usuario
    /// </summary>
    public async Task<string> GenerateNewUser(string personName, string personEmail, string roleName)
    {
      var user = new UserModel
      {
        UserName = GenerateUniqueUsername(personName),
        Email = personEmail,
        EmailConfirmed = true,
        Id = Guid.NewGuid().ToString(),
        SecurityStamp = Guid.NewGuid().ToString()
      };
      var password = GenerateRandomPassword();
      var result = await _userManager.CreateAsync(user, password);

      if (!result.Succeeded)
        throw new Exception(result.Errors.First().ToString());

      await PutUserRole(user.UserName, roleName);

      await _emailService.SendEmailAsync(
      user.Email,
      Resources.SuccessfullyCreatedUser,
      string.Format(Resources.UserEmailBody, personName, user.UserName, password, _configuration["BaseDomain"], _configuration["SystemName"])
      );
      return user.Id;
    }

    /// <summary>
    /// Gera um novo nome de usuario
    /// </summary>
    private string GenerateUniqueUsername(string personName)
    {
      string username = GenerateUsernameFromName(personName);
      bool usernameExists = _userManager.Users.Any(u => u.UserName == username);

      int counter = 1;
      while (usernameExists)
      {
        username = GenerateUsernameFromName(personName) + counter;
        usernameExists = _userManager.Users.Any(u => u.UserName == username);
        counter++;
      }

      return username;
    }

    private static string GenerateUsernameFromName(string personName)
    {
      string lowercaseName = personName.ToLower();
      string cleanedName = Regex.Replace(lowercaseName, "[^a-z0-9]", "");
      string username = cleanedName.Substring(0, Math.Min(cleanedName.Length, 8));

      return username;
    }

    private static string GenerateRandomPassword()
    {
      var random = new Random();
      const string specialCharacters = "!@#$%^&*()_+-=[]{}|;:,.<>?";

      string password = "";

      password += GetRandomChar("0123456789", random);
      password += GetRandomChar("abcdefghijklmnopqrstuvwxyz", random);
      password += GetRandomChar("ABCDEFGHIJKLMNOPQRSTUVWXYZ", random);
      password += GetRandomChar(specialCharacters, random);

      for (int i = 4; i < 8; i++)
      {
        password += GetRandomChar("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" + specialCharacters, random);
      }

      password = new string(password.ToCharArray().OrderBy(x => random.Next()).ToArray());

      return password;
    }

    private static char GetRandomChar(string characters, Random random)
    {
      return characters[random.Next(characters.Length)];
    }
  }
}
