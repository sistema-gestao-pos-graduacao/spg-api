using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPG.Domain.Model;
using SPG.Application.Properties;

namespace SPG.Application.Services
{
  public class UserService(UserManager<UserModel> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper) : IUserService
  {
    private readonly UserManager<UserModel> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IMapper _mapper = mapper;

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

      await AtribuirRoleAoUsuario(user.UserName, userDto.Role);
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

    public async Task AtribuirRoleAoUsuario(string nomeDoUsuario, string nomeDaRole)
    {
      var usuario = await _userManager.FindByNameAsync(nomeDoUsuario);
      var role = await _roleManager.FindByNameAsync(nomeDaRole);

      if (usuario != null && role != null)
      {
        await _userManager.AddToRoleAsync(usuario, nomeDaRole);
      }
    }
  }
}
